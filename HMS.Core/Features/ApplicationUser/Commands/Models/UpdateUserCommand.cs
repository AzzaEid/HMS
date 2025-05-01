using HMS.Core.Bases;
using HMS.Data.Entities.Enums;
using MediatR;

namespace HMS.Core.Features.ApplicationUser.Commands.Models
{
    public class UpdateUserCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public Gender Gender { get; set; }
        public string? ContactNumber { get; set; }
    }
}
