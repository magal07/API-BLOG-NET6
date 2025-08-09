using Blog.Extensions;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Controllers;

[ApiController]
public class CategoryController : ControllerBase
{
    #region Controllers - CRUD

    [HttpGet("v1/categories")]
    public async Task<ActionResult> GetAsync([FromServices] BlogDataContext context)
    {
        try
        {
            var categories = await context.Categories.ToListAsync();
            return Ok(new ResultViewModel<IList<Category>>(categories));
        }
        catch
        {
            return StatusCode(500, (new ResultViewModel<IList<Category>>("GENEX500 - Internal server error")));
        }
    }

    [HttpGet("v1/categories/{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] BlogDataContext context)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null) return NotFound(new ResultViewModel<Category>("GENEXP499 - Not found content"));
            return Ok(new ResultViewModel<Category>(category));
        }
        catch (Exception ex) 
        {
         return StatusCode(500, "GENEX500 - Internal server error");   
        }
    }

    [HttpPost("v1/categories")]
    public async Task<IActionResult> PostAsync([FromServices] BlogDataContext context, [FromBody] EditorCategoryViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));
        
        try
        {
            // Diminuindo parâmetros através da CreateCategoryViewModel, filtrando somente (nome, slug) sem o posts
            var category = new Category
            {
                Id = 0,
                Name = model.Name,
                Slug = model.Slug.ToLower()
            };
            
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            return Created($"v1/categories/{category.Id}", new ResultViewModel<Category>(category));
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, new ResultViewModel<Category>("DBEX02 - Not is possible include category."));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Category>("GENEX500 - Internal server error"));
        }
    }

    [HttpPut("v1/categories/{id:int}")]
    public async Task<IActionResult> PutAsync([FromServices] BlogDataContext context, [FromRoute] int id, [FromBody] EditorCategoryViewModel model)
    {
        try
        {
            // Buscando Id na Entidade Categories através do contexto
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null) return NotFound(new ResultViewModel<Category>("Not found content"));

            // Passando parâmetros do que será alterado
            category.Name = model.Name;
            category.Slug = model.Slug;

            // Atualizando e salvando novo status dos parâmetros alterados
            context.Categories.Update(category);
            await context.SaveChangesAsync();
            return Ok(new ResultViewModel<Category>(category));
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return StatusCode(500, new ResultViewModel<Category>("DBEX02 - Not is possible to alter category."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<Category>("GENEX500 - Internal server error"));
        }
    }

    [HttpDelete("v1/categories/{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromServices] BlogDataContext context, [FromRoute] int id)
    {
        try
        {
            // Buscando id da categoria no contexto
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null) return NotFound(new ResultViewModel<Category>("Not found content"));

            // Removendo e posteriormente salvando categoria pelo id (baseado no contexto)
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return Ok(new ResultViewModel<Category>(category));
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return StatusCode(500, new ResultViewModel<Category>("DBEX02 - Not is possible to delete category."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<Category>("GENEX500 - Internal server error"));
        }
    }
    #endregion 
}
 