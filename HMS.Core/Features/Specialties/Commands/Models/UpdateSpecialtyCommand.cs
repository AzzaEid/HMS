using HMS.Core.Bases;
using MediatR;

namespace HMS.Core.Features.Specialties.Commands.Models
{
    public class UpdateSpecialtyCommand : IRequest<Response<string>>
    {
        public int SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }
    }
}
