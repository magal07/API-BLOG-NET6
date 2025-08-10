using Blog.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Data.Controllers;

[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    #region Verificando healt-life da rota. Pode-se verificar será através do cmd no comando ping + a rota ou Postman/Insomnia
    [HttpGet("")]
    [ApiKey]
    public IActionResult Get()
    {
        return Ok();
    }
    #endregion
}