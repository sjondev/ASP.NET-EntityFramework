using BlogApi.Data;
using BlogApi.Extensions;
using BlogApi.Models;
using BlogApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("v1/categories")]
        async public Task<IActionResult> GetAsync(
            [FromServices] DataContext context
        )
        {
            try 
            {
                var categoriesGet = await context.Categories.AsNoTracking().ToListAsync();
                return Ok(new ResultViewModel<List<Category>>(categoriesGet));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("5x04 - Falha interna no servidor"));
            }
        }

        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            int id, 
            [FromServices] DataContext context
        )
        {
            try
            {
                var category = await context
                    .Categories
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (category == null) return NotFound(new ResultViewModel<Category>("Conteúdo não encontrado"));
                return Ok(new ResultViewModel<Category>(category));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<Category>("5x05 - Falha interna no servidor"));
            }
        }

        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostCategoryAsync (
            [FromBody] EditorCategoryViewModel model,
            [FromServices] DataContext context
        )
        {
            if (!ModelState.IsValid) 
                return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));

            try
            {
                var category = new Category
                {
                    Id = 0,
                    Name = model.Name,
                    Slug = model.Slug.ToLower()
                };
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                return Created($"v1/categories/{category.Id}", model);
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<Category>("5x06 - Erro interno do Servidor"));
            }
        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutCategoryAsync(
            int id, 
            [FromBody] EditorCategoryViewModel model, 
            [FromServices] DataContext context
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));

            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null) return NotFound();

                category.Name = model.Name;
                category.Slug = model.Slug;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<Category>("5x07 - Erro interno do Servidor"));
            }
        }



        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteCategoryAsync(
            int id, 
            [FromServices] DataContext context
        )
        {
            try
            {
                var category = await context
                    .Categories
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (category == null) return NotFound();

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>("Categoria removida com sucesso!"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<Category>("5x08 - Erro interno do Servidor"));
            }
        }
    }
}
