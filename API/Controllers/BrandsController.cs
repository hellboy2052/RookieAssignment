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
    public class BrandsController : BaseController
    {
        private readonly MyDbContext _myDbContext;

        public BrandsController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<BrandVm>>> GetBrands()
        {
            return await _myDbContext.Brands.Select(x => new BrandVm
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BrandVm>> GetBrand(int id)
        {
            var brand = await _myDbContext.Brands.FindAsync(id);

            if(brand == null) return NotFound();

            var brandVm = new BrandVm{
                Id = brand.Id,
                Name = brand.Name
            };

            return brandVm;
        }

        [HttpPost]
        public async Task<ActionResult> CreateBrand(BrandFormVm brandFormVm){

            var brand = new Brand{
                Name = brandFormVm.Name
            };

            _myDbContext.Brands.Add(brand);

            await _myDbContext.SaveChangesAsync();

            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBrand(int id)
        {
            var brand = await _myDbContext.Brands.FindAsync(id);

            if (brand == null) { return NotFound(); };

            _myDbContext.Brands.Remove(brand);

            await _myDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}