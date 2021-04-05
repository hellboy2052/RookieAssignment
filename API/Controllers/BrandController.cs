using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.models;
using API.Services.Brands;
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
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BrandVm>> GetBrand(int id)
        {
            return await Mediator.Send(new Detail.Query{Id = id});
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand(BrandFormVm brandFormVm)
        {
            return Ok(await Mediator.Send(new Create.Command{brandFormVm = brandFormVm}));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            return Ok(await Mediator.Send(new Delete.Command{Id = id}));

        }

    }
}