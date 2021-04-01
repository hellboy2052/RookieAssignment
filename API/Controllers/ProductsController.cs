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
            return await _myDbContext.Products.Select(x => new ProductVm
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Description = x.Description,
                Image = x.Image
            }).ToListAsync();
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

        [HttpGet("category")]
        public async Task<ActionResult<IList<ProductVm>>> GetByCategory(string n)
        {
            var Category = await _myDbContext.Categories.Where(x => x.Name == n)
                                                        .Select(x => x)
                                                        .FirstOrDefaultAsync<Category>();

            if (Category == null) return null;

            return await _myDbContext.Products.Where(x => x.CategoryId == Category.Id).Select(x => new ProductVm
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Description = x.Description,
                Image = x.Image
            }).ToListAsync();

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
                CategoryId = productFormVm.CategoryId
            };

            _myDbContext.Products.Add(product);

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