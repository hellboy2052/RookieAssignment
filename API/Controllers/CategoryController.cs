using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Domain;
using API.Services.Categories;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }
        
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            return HandleResult(await Mediator.Send(new Detail.Query{Id = id}));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryFormVm CategoryFormVm){

            return HandleResult(await Mediator.Send(new Create.Command{categoryFormVm = CategoryFormVm}));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command{Id = id}));
        }
    }
}