using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.models;
using API.Services.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareVM;

namespace API.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly MyDbContext _myDbContext;

        public ProductsController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductVm>>> GetProducts()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVm>> GetProduct(int id)
        {
            return await Mediator.Send(new Detail.Query{Id = id});
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductFormVm productFormVm)
        {


            return Ok(await Mediator.Send(new Create.Command{product = productFormVm}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            return Ok(await Mediator.Send(new Delete.Command{Id = id}));
        }
    }
}