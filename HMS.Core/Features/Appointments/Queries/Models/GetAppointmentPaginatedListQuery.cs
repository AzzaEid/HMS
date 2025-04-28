using HMS.Core.Features.Appointments.Queries.Results;
using HMS.Core.Wrappers;
using HMS.Data.Helper;
using MediatR;

namespace HMS.Core.Features.Appointments.Queries.Models
{
    public class GetAppointmentPaginatedListQuery : IRequest<PaginatedResult<GetAppointmentPaginatedListResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public AppointmentOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }
    }

}
