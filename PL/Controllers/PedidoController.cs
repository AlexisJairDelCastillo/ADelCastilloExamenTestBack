using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class PedidoController : Controller
    {
        private IHostingEnvironment environment;
        private IConfiguration configuration;

        public PedidoController(IHostingEnvironment _environment, IConfiguration _configuration)
        {
            environment = _environment;
            configuration = _configuration;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string correoElectronico, string contrasenia)
        {
            ML.Result result = BL.Cliente.Login(correoElectronico);
            if (result.Correct)
            {
                ML.Cliente cliente = (ML.Cliente)result.Object;

                if (contrasenia == cliente.Contrasenia)
                {
                    HttpContext.Session.SetInt32("IdCliente", cliente.IdCliente);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "Contraseña incorrecta. Intentalo de nuevo.";
                    return PartialView("ModalLogin");
                }
            }
            else
            {
                ViewBag.Message = "Usuario incorrecto. intentalo de nuevo.";
                return PartialView("ModalLogin");
            }
        }

        public ActionResult GetAll()
        {

            ML.Pedido pedido = new ML.Pedido();

            ML.Result resultPedido = new ML.Result();
            resultPedido.Objects = new List<Object>();

            using (HttpClient client = new HttpClient())
            {
                string webApi = configuration["WebApi"];
                client.BaseAddress = new Uri(webApi);

                var responseTask = client.GetAsync("pedido/getall");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Pedido resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Pedido>(resultItem.ToString());
                        resultPedido.Objects.Add(resultItemList);
                    }
                }

                pedido.Pedidos = resultPedido.Objects;
            }
            return View(pedido);
        }

        [HttpGet]
        public ActionResult Delete(ML.Pedido pedido)
        {
            ML.Result resultAlumno = new ML.Result();
            int idPedido = pedido.IdPedido;

            using (HttpClient client = new HttpClient())
            {
                string webApi = configuration["WebApi"];
                client.BaseAddress = new Uri(webApi);

                var responseTask = client.DeleteAsync("pedido/delete/" + idPedido);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Titulo = "El registro se elimino correctamente.";
                    return View("Modal");
                }
                else
                {
                    ViewBag.Titulo = "Ocurrio un error al eliminar el registro.";
                    return View("Modal");
                }
            }
        }

        public ActionResult GetPedidoDetalle()
        {

            ML.PedidoDetalle pedidoDetalle = new ML.PedidoDetalle();

            ML.Result resultPedidoDetalle = new ML.Result();
            resultPedidoDetalle.Objects = new List<Object>();

            using (HttpClient client = new HttpClient())
            {
                string webApi = configuration["WebApi"];
                client.BaseAddress = new Uri(webApi);

                var responseTask = client.GetAsync("pedidodetalle/getall");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.PedidoDetalle resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.PedidoDetalle>(resultItem.ToString());
                        resultPedidoDetalle.Objects.Add(resultItemList);
                    }
                }

                pedidoDetalle.PedidosDetalles = resultPedidoDetalle.Objects;
            }
            return View(pedidoDetalle);
        }

        [HttpGet]
        public ActionResult Form(int? idPedido)
        {
            ML.Pedido pedido = new ML.Pedido();
            ML.Result resultCliente = BL.Cliente.GetAll();
            ML.Result resultProducto = BL.Producto.GetAll();

            pedido.Cliente = new ML.Cliente();
            pedido.Producto = new ML.Producto();

            pedido.Cliente.Clientes = resultCliente.Objects;
            pedido.Producto.Productos = resultProducto.Objects;

            ViewBag.Titulo = "Registrar un nuevo Alumno";
            ViewBag.Accion = "Agregar";

            return View(pedido);

        }

        [HttpPost]
        public ActionResult Form(ML.Pedido pedido)
        {
            ML.Result result = new ML.Result();

            using (HttpClient client = new HttpClient())
            {
                string webApi = configuration["WebApi"];
                client.BaseAddress = new Uri(webApi);

                Task<HttpResponseMessage> postTask = client.PostAsJsonAsync<ML.Pedido>("pedidodetalle/add", pedido);
                postTask.Wait();

                HttpResponseMessage resultTask = postTask.Result;
                if (resultTask.IsSuccessStatusCode)
                {
                    result.Correct = true;
                    ViewBag.Titulo = "El registro se inserto correctamente.";
                    ViewBag.Message = result.Message;
                    return View("Modal");
                }
                else
                {
                    ViewBag.Titulo = "Ocurrio un error al insertar el registro.";
                    ViewBag.Message = result.Message;
                    return View("Modal");
                }
            }
        }
    }
}
