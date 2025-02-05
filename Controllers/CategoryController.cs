using BlogApi.Data;
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
                return Ok(categoriesGet);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error Server:" + e.Message);
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
                var categoryId = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (categoryId == null) return NotFound();
                return Ok(categoryId);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error Server:" + e.Message);
            }
        }

        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostCategoryAsync (
            [FromBody] EditorCategoryViewModel model,
            [FromServices] DataContext context
        )
        {
            // O asp.net ja faz isso por padrão o que ele faz é o seguinte.
            // Verifica os campos que são requiridos caso eles não estejam presentes com valores
            // ele retorna um BadRequest
            if (ModelState.IsValid) return BadRequest();

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
                return StatusCode(500, "Error Server:" + e.Message);
            }
        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutCategoryAsync(
            int id, 
            [FromBody] EditorCategoryViewModel model, 
            [FromServices] DataContext context
        )
        {
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
                return StatusCode(500, "Error Server:" + e.Message);
            }
        }



        [HttpDelete("v1/categorires/{id:int}")]
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

                return Ok("Category removed with success!");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error Server:" + e.Message);
            }
        }
    }
}
