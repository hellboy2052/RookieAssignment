using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return await _myDbContext.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _myDbContext.Products.FindAsync(id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var Product = await _myDbContext.Products.FindAsync(id);

            if(Product == null) return NotFound();

            _myDbContext.Products.Remove(Product);

            await _myDbContext.SaveChangesAsync();
            
            return Accepted();
        }
    }
}