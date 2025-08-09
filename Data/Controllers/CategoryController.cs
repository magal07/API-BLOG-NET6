using Microsoft.AspNetCore.Mvc;

namespace Blog.Data.Controllers;

[ApiController]
public class CategoryController : ControllerBase
{
    [HttpGet("v1/categories")]
    public IActionResult Get([FromServices] BlogDataContext context)
    {
        var categories = context.Categories.ToList();
        return Ok(categories);
    }
}