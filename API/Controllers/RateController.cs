using System.Threading.Tasks;
using API.Services.Rates;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RateController : BaseController
    {
        public RateController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> rate(int id, double rate){
            return HandleResult(await Mediator.Send(new RatingToggle.Command{productID = id, rate = rate}));
        }
    }
}