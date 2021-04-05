using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.models;
using API.Services.Categories;
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
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryVm>> GetCategory(int id)
        {
            return await Mediator.Send(new Detail.Query{Id = id});
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory(CategoryFormVm CategoryFormVm){

            return Ok(await Mediator.Send(new Create.Command{categoryFormVm = CategoryFormVm}));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            return Ok(await Mediator.Send(new Delete.Command{Id = id}));
        }
    }
}