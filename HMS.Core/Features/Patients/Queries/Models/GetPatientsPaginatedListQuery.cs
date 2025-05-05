using HMS.Core.Features.Patients.Queries.Results;
using HMS.Core.Wrappers;
using HMS.Data.Helper.Enums;
using MediatR;

namespace HMS.Core.Features.Patients.Queries.Models
{
    public class GetPatientPaginatedListQuery : IRequest<PaginatedResult<GetPatientPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PatientOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
