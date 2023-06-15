using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class testeController : ControllerBase
    {
        [HttpGet]
        public IActionResult teste()
        {
            return Ok(new {teste = "Funcionou!"});
        }
    }
}
