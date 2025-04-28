using HMS.Core.Bases;
using HMS.Core.Features.Appointments.Queries.Results;
using MediatR;

namespace HMS.Core.Features.Appointments.Queries.Models
{
    public class GetAppointmentByIdQuery : IRequest<Response<GetAppointmentDetailDto>>
    {
        public int Id { get; set; }
    }

}
