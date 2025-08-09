using Microsoft.AspNetCore.Mvc;

namespace Blog.Data.Controllers;

[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    // verificando se a rota tá ok, possivelmente quem for verificar será através do cmd no comando ping
    [HttpGet("")]
    public IActionResult Get()
    {
        return Ok();
    }
}