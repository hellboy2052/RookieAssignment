using System.Threading.Tasks;
using API.Services.Profiles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        public ProfileController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            return HandleResult(await Mediator.Send(new Detail.Query()));
        }
        
        
        [HttpPost("AddToCart/{productId}")]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 0){
            return HandleResult(await Mediator.Send(new Carting.Command{productId = productId, quantity = quantity}));
        }

        [HttpDelete("DeleteFromCart/{productId}")]
        public async Task<IActionResult> DeleteFromCart(int productId){
            return HandleResult(await Mediator.Send(new RemoveItem.Command{productId = productId}));
        }
        
    }
}