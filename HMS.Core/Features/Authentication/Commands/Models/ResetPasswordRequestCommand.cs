using HMS.Core.Bases;
using MediatR;

namespace HMS.Core.Features.Authentication.Commands.Models
{
    public class ResetPasswordRequestCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
    }
}
