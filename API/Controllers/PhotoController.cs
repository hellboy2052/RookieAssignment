using System.Threading.Tasks;
using API.Services.Photo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Policy = "IsPermitRequire")]
    public class PhotoController : BaseController
    {
        public PhotoController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] Add.Command command)
        {
            return HandleResult(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, int proId)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id, proId = proId }));
        }

        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMain(string id, int proId)
        {
            return HandleResult(await Mediator.Send(new SetMain.Command { Id = id, proId = proId }));
        }
    }
}