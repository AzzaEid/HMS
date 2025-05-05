using HMS.API.Base;
using HMS.Core.Features.Authorization.Commands.Models;
using HMS.Core.Features.Authorization.Queries.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AuthorizationController : AppControllerBase
    {
        [HttpPost("role")]
        public async Task<IActionResult> Add([FromForm] AddRoleCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpPut("role")]
        public async Task<IActionResult> Edit([FromForm] EditRoleCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete("role/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await Mediator.Send(new DeleteRoleCommand(id));
            return NewResult(response);
        }
        [HttpGet("role")]
        public async Task<IActionResult> GetRoleList()
        {
            var response = await Mediator.Send(new GetRolesListQuery());
            return NewResult(response);
        }
        [HttpGet("role/{id}")]
        public async Task<IActionResult> GetRoleById([FromRoute] int id)
        {
            var response = await Mediator.Send(new GetRoleByIdQuery() { Id = id });
            return NewResult(response);
        }

        [SwaggerOperation(Summary = "Manage user roles", OperationId = "ManageUserRoles")]
        [HttpGet("role/manage/{userId}")]
        public async Task<IActionResult> ManageUserRoles([FromRoute] int userId)
        {
            var response = await Mediator.Send(new ManageUserRolesQuery() { UserId = userId });
            return NewResult(response);
        }
        [HttpPost("role/update")]
        public async Task<IActionResult> UpateUserRoles([FromBody] UpdateUserRolesCommand userRolesCommand)
        {
            var response = await Mediator.Send(userRolesCommand);
            return NewResult(response);
        }
        [HttpGet("claim/manage/{userId}")]
        public async Task<IActionResult> ManageUserClaims([FromRoute] int userId)
        {
            var response = await Mediator.Send(new ManageUserClaimsQuery() { UserId = userId });
            return NewResult(response);
        }

    }
}
