using HMS.Core.Bases;
using HMS.Core.Features.Specialties.Queries.Results;
using MediatR;

namespace HMS.Core.Features.Specialties.Queries.Models
{
    public class GetSpecialtyByIdQuery : IRequest<Response<GetSpecialtyByIdResponse>>
    {
        public int SpecialtyId { get; set; }
    }
}
