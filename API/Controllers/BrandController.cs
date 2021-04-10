using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.models;
using API.Services.Brands;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrand(int id)
        {
            return HandleResult(await Mediator.Send(new Detail.Query{Id = id}));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBrand(BrandFormVm brandFormVm)
        {
            return HandleResult(await Mediator.Send(new Create.Command{brandFormVm = brandFormVm}));

        }
        
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command{Id = id}));

        }

    }
}