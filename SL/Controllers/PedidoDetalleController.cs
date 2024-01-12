using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/pedidodetalle")]
    [ApiController]
    public class PedidoDetalleController : ControllerBase
    {
        [HttpGet]
        [Route("getall")]
        public IActionResult Getall()
        {
            ML.Result result = BL.PedidoDetalle.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(ML.Pedido pedido)
        {
            ML.Result result = BL.PedidoDetalle.Add(pedido);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
