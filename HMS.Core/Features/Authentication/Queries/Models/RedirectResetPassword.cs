using HMS.Core.Bases;
using MediatR;

namespace HMS.Core.Features.Authentication.Queries.Models
{
    public class RedirectResetPassword : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
