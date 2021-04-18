using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Domain;
using API.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareVM;
using MediatR;

namespace API.Controllers
{
    
    public class ProductsController : BaseController
    {
        public ProductsController(IMediator mediator) : base(mediator)
        {
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            return HandleResult(await Mediator.Send(new Detail.Query{Id = id}));
        }

        [Authorize(Policy = "IsPermitRequire")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductFormVm productFormVm)
        {


            return HandleResult(await Mediator.Send(new Create.Command{product = productFormVm}));
        }
        [Authorize(Policy = "IsPermitRequire")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            return HandleResult(await Mediator.Send(new Delete.Command{Id = id}));
        }

        
    }
}