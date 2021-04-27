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
using API.Services.Photo;

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
            return HandleResult(await Mediator.Send(new Detail.Query { Id = id }));
        }

        // Create Product
        [Authorize(Policy = "IsPermitRequire")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ProductFormVm productFormVm)
        {
            // Create product
            var result = await Mediator.Send(new Create.Command { product = productFormVm });

            if (!result.IsSuccess)
            {
                return HandleResult(result);
            }

            // get id for adding picture
            var proId = result.Value;

            // Check if picture empty or not
            if ((productFormVm.Pictures == null))
            {
                return HandleResult(result);
            }

            if (productFormVm.Pictures.Count < 1)
            {
                return HandleResult(result);
            }
            // Create picture
            foreach (var pic in productFormVm.Pictures)
            {
                var result2 = await Mediator.Send(new Add.Command { productId = proId, File = pic });

                // Check error
                if (!result2.IsSuccess)
                {
                    return HandleResult(result2);
                }
            }
            return HandleResult(result);
        }

        // Edit Product
        [Authorize(Policy = "IsPermitRequire")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditProduct(int id, [FromForm] ProductFormVm productFormVm)
        {
            productFormVm.Id = id;
            var result = await Mediator.Send(new Edit.Command { Product = productFormVm });

            if (!result.IsSuccess)
            {
                return HandleResult(result);
            }

            // Check if picture empty or not
            if ((productFormVm.Pictures == null))
            {
                return HandleResult(result);
            }

            if (productFormVm.Pictures.Count < 1)
            {
                return HandleResult(result);
            }
            foreach (var pic in productFormVm.Pictures)
            {
                var result2 = await Mediator.Send(new Add.Command { productId = productFormVm.Id, File = pic });

                // Check error
                if (!result2.IsSuccess)
                {
                    return HandleResult(result2);
                }
            }

            return HandleResult(result);
        }

        // Delete Product
        [Authorize(Policy = "IsPermitRequire")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            // Delete picture first then product
            var result = await Mediator.Send(new API.Services.Photo.DeleteAll.Command { proId = id });

            if (!result.IsSuccess)
            {
                return HandleResult(result);
            }

            return HandleResult(await Mediator.Send(new API.Services.Products.Delete.Command { Id = id }));
        }


    }
}