using System.Collections.Generic;
using System.Threading.Tasks;
using API.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using ShareVM;

namespace API.Controllers
{
    public class OrderController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetOrders(){
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetail(int id){
            return HandleResult(await Mediator.Send(new Detail.Query{orderId = id}));
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> CheckOut(List<int> productIds){
            return HandleResult(await Mediator.Send(new CheckOut.Command{productIds = productIds}));
        }
    }
}