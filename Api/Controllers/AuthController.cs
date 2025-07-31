using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CadDoctor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet("ping")]
        [Authorize]
        public IActionResult Ping()
        {
            return Ok("AuthController está funcionando!");
        }
    }
}
