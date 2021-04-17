using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Domain;
using API.Services.Brands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareVM;
using MediatR;

namespace API.Controllers
{
    public class BrandController : BaseController
    {

        public BrandController(IMediator mediator) :base(mediator)
        {
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