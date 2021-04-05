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
    public class BrandController : BaseController
    {
        private readonly MyDbContext _context;

        public BrandController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<BrandVm>>> GetBrands()
        {
            return await _context.Brands.Select(x => new BrandVm
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BrandVm>> GetBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);

            if (brand == null) return NotFound();

            var brandVm = new BrandVm { Name = brand.Name };

            return brandVm;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand(BrandFormVm brandFormVm)
        {
            var brand = new Brand
            {
                Name = brandFormVm.Name
            };

            _context.Brands.Add(brand);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Accepted();

            return BadRequest();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);

            if (brand == null) return NotFound();

            _context.Brands.Remove(brand);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return NoContent();

            return BadRequest();

        }

    }
}