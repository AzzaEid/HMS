using HMS.API.Base;
using HMS.Core.Features.Authorization.Commands.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authorization : AppControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] AddRoleCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
    }
}
