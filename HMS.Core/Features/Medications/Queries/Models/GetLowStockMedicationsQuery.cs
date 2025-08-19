using HMS.Core.Bases;
using HMS.Core.Features.Medications.Queries.Results;
using MediatR;

namespace HMS.Core.Features.Medications.Queries.Models
{
    public class GetLowStockMedicationsQuery : IRequest<Response<List<GetMedicationListResponse>>>
    {
        public int Threshold { get; set; } = 10;
    }

}
