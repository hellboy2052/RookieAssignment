using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class BrandsController : BaseController
    {
        private readonly MyDbContext _myDbContext;

        public BrandsController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Brand>>> GetBrands()
        {
            return await _myDbContext.Brands.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
            return await _myDbContext.Brands.FindAsync(id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBrand(int id)
        {
            var brand = await _myDbContext.Brands.FindAsync(id);

            if (brand == null) { return NotFound(); };

            _myDbContext.Brands.Remove(brand);

            await _myDbContext.SaveChangesAsync();

            return Accepted();
        }
    }
}