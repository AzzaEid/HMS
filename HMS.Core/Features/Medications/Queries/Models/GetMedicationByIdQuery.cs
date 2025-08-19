using HMS.Core.Bases;
using HMS.Core.Features.Medications.Queries.Results;
using MediatR;

namespace HMS.Core.Features.Medications.Queries.Models
{
    public class GetMedicationByIdQuery : IRequest<Response<GetMedicationByIdResponse>>
    {
        public int MedicationId { get; set; }
    }
}
