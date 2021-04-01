using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareVM;

namespace API.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly MyDbContext _myDbContext;

        public CategoriesController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryVm>>> GetCategories()
        {
            return await _myDbContext.Categories.Select(x => new CategoryVm
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryVm>> GetCategory(int id)
        {
            var Category = await _myDbContext.Categories.FindAsync(id);

            if(Category == null) return NotFound();

            var CategoryVm = new CategoryVm{
                Id = Category.Id,
                Name = Category.Name
            };

            return CategoryVm;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory(CategoryFormVm CategoryFormVm){

            var Category = new Category{
                Name = CategoryFormVm.Name
            };

            _myDbContext.Categories.Add(Category);

            await _myDbContext.SaveChangesAsync();

            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var Category = await _myDbContext.Categories.FindAsync(id);

            if (Category == null) { return NotFound(); };

            _myDbContext.Categories.Remove(Category);

            await _myDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}