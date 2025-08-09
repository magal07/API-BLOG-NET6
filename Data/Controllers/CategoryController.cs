using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Controllers;

[ApiController]
public class CategoryController : ControllerBase
{
    #region Controllers - CRUD
    [HttpGet("v1/categories")]
    public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context)
    {
        var categories = await context.Categories.ToListAsync();
        return Ok(categories);
    }

    [HttpGet("v1/categories/{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] BlogDataContext context)
    {
        var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category == null) return NotFound();
        return Ok(category);
    }

    [HttpPost("v1/categories")]
    public async Task<IActionResult> PostAsync([FromServices] BlogDataContext context, [FromBody] Category model)
    {
        try
        {
            await context.AddAsync(model);
            await context.SaveChangesAsync();
            return Created($"v1/categories/{model.Id}", model);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, "05XE9 - Not is possible include category.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "05xe7 - Internal server error");
        }
    }

    [HttpPut("v1/categories/{id:int}")]
    public async Task<IActionResult> PutAsync([FromRoute] int id, [FromServices] BlogDataContext context, Category model)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null) return NotFound();

            category.Name = model.Name;
            category.Slug = model.Slug;

            context.Categories.Update(category);
            await context.SaveChangesAsync();
            return Ok(category);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return StatusCode(500, "05XE5 - Not is possible to alter category.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "05xe9 - Internal server error");
        }
    }

    [HttpDelete("v1/categories/{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] BlogDataContext context)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null) return NotFound();

            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        
            return Ok(category.Name);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return StatusCode(500, "05XE2 - Not is possible to delete category.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "05xe1 - Internal server error");
        }
    }
    #endregion 
}
 