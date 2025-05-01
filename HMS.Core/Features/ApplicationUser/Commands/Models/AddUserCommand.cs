using HMS.Core.Bases;
using MediatR;

namespace HMS.Core.Features.ApplicationUser.Commands.Models
{
    public class AddUserCommand : IRequest<Response<string>>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? ContactNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
