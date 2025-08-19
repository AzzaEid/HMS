using HMS.Core.Bases;
using MediatR;

namespace HMS.Core.Features.Specialties.Commands.Models
{

    public class DeleteSpecialtyCommand : IRequest<Response<string>>
    {
        public int SpecialtyId { get; set; }
    }
}
