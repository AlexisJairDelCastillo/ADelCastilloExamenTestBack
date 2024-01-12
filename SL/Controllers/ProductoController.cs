using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/producto")]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        [HttpGet]
        [Route("getall")]
        public IActionResult Getall()
        {
            ML.Result result = BL.Producto.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
    }
}
