using HMS.API.Base;
using HMS.Core.Features.Authentication.Commands.Models;
using HMS.Core.Features.Authentication.Queries.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> SignIn([FromForm] SignInCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet("validate-token")]
        public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery query)
        {
            var response = await Mediator.Send(query);
            return NewResult(response);
        }
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost("reset-password-request")]
        public async Task<IActionResult> ResetPasswordRequest([FromQuery] ResetPasswordRequestCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpGet("reset-password")]
        public IActionResult RedirectResetPassword([FromQuery] RedirectResetPassword query)
        {
            /// add front end reset page link here
            //var encodedToken = System.Web.HttpUtility.UrlEncode(query.Token);
            return Ok(query);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }



    }
}
