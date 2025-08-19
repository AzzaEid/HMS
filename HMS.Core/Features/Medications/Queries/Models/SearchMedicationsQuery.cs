using HMS.Core.Bases;
using HMS.Core.Features.Medications.Queries.Results;
using MediatR;

namespace HMS.Core.Features.Medications.Queries.Models
{
    public class SearchMedicationsQuery : IRequest<Response<List<GetMedicationListResponse>>>
    {
        public string SearchTerm { get; set; }
    }
}
