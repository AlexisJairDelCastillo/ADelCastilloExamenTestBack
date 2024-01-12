using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        private IHostingEnvironment environment;
        private IConfiguration configuration;

        public ProductoController(IHostingEnvironment _environment, IConfiguration _configuration)
        {
            environment = _environment;
            configuration = _configuration;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Producto producto = new ML.Producto();

            ML.Result resultProducto = new ML.Result();
            resultProducto.Objects = new List<object>();

            using (HttpClient client = new HttpClient())
            {
                string webApi = configuration["WebApi"];
                client.BaseAddress = new Uri(webApi);

                var responseTask = client.GetAsync("producto/getall");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var item in readTask.Result.Objects)
                    {
                        ML.Producto itemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(item.ToString());
                        resultProducto.Objects.Add(itemList);
                    }
                }

                producto.Productos = resultProducto.Objects;
            }

            return View(producto);
        }
    }
}
