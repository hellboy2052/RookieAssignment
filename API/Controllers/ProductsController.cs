using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.models;
using API.Services.Product;
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
            var product = await _myDbContext.Products.FindAsync(id);

            if (product == null) return NotFound();

            var productVm = new ProductVm
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Image = product.Image
            };

            return productVm;
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductFormVm productFormVm)
        {


            var product = new Product
            {
                Name = productFormVm.Name,
                Price = productFormVm.Price,
                Description = productFormVm.Description,
                Image = productFormVm.Image,
                BrandId = productFormVm.BrandId,
                CreatedDate = DateTime.Now
            };

            foreach (var cate in productFormVm.CategoryName)
            {
                var category = await _myDbContext.Categories
                    .FirstOrDefaultAsync(x => x.Name == cate);

                if (category == null) return BadRequest("Invalid category");

                product.ProductCategories.Add(
                    new CategoryProduct
                    {
                        Category = category,
                        Product = product
                    }
                );
            }

            _myDbContext.Add(product);

            await _myDbContext.SaveChangesAsync();

            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var Product = await _myDbContext.Products.FindAsync(id);

            if (Product == null) return NotFound();

            _myDbContext.Products.Remove(Product);

            await _myDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}