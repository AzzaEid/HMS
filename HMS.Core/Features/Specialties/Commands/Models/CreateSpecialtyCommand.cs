using HMS.Core.Bases;
using MediatR;

namespace HMS.Core.Features.Specialties.Commands.Models
{
    public class CreateSpecialtyCommand : IRequest<Response<string>>
    {
        public string SpecialtyName { get; set; }
    }
}
