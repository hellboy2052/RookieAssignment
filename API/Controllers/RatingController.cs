using System.Threading.Tasks;
using API.Services.Ratings;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RatingController : BaseController
    {
        [HttpPost("{id}")]
        public async Task<IActionResult> rate(int id, double rate){
            return HandleResult(await Mediator.Send(new RatingToggle.Command{productID = id, rate = rate}));
        }
        
    }
}