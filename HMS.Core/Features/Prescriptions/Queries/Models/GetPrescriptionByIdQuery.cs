using HMS.Core.Bases;
using HMS.Core.Features.Prescriptions.Queries.Results;
using MediatR;

namespace HMS.Core.Features.Prescriptions.Queries.Models
{
    public class GetPrescriptionByIdQuery : IRequest<Response<GetPrescriptionByIdResponse>>
    {
        public int Id { get; set; }
    }
}
