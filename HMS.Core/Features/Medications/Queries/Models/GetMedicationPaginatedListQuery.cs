using HMS.Core.Features.Medications.Queries.Results;
using HMS.Core.Wrappers;
using HMS.Data.Helper.Enums;
using MediatR;

namespace HMS.Core.Features.Medications.Queries.Models
{
    public class GetMedicationPaginatedListQuery : IRequest<PaginatedResult<GetMedicationListResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public MedicationOrderingEnum OrderBy { get; set; } = MedicationOrderingEnum.NameAsc;
        public string? Search { get; set; }
    }
}
