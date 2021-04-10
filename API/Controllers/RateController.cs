using System.Threading.Tasks;
using API.Services.Rates;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RateController : BaseController
    {
        [HttpPost("{id}")]
        public async Task<IActionResult> rate(int id, double rate){
            return HandleResult(await Mediator.Send(new RatingToggle.Command{productID = id, rate = rate}));
        }
    }
}