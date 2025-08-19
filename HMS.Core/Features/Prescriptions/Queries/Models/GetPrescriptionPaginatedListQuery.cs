using HMS.Core.Features.Prescriptions.Queries.Results;
using HMS.Core.Wrappers;
using HMS.Data.Helper.Enums;
using MediatR;

namespace HMS.Core.Features.Prescriptions.Queries.Models
{
    public class GetPrescriptionPaginatedListQuery : IRequest<PaginatedResult<GetPrescriptionListResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public PrescriptionOrderingEnum OrderBy { get; set; } = PrescriptionOrderingEnum.DateDesc;
        public string? Search { get; set; }
    }
}
