using HMS.Core.Bases;
using HMS.Core.Features.Medications.Queries.Results;
using MediatR;

namespace HMS.Core.Features.Medications.Queries.Models
{
    public class GetMedicationListQuery : IRequest<Response<List<GetMedicationListResponse>>>
    {
    }
}
