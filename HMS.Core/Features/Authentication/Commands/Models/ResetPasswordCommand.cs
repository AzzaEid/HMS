using HMS.Core.Bases;
using MediatR;

namespace HMS.Core.Features.Authentication.Commands.Models
{
    public class ResetPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
    }
}
