using HMS.Core.Bases;
using HMS.Core.Features.Appointments.Queries.Results;
using MediatR;

namespace HMS.Core.Features.Appointments.Queries.Models
{
    public class GetDoctorAppointmentsQuery : IRequest<Response<List<GetAppointmentListDto>>>
    {
        public int DoctorId { get; set; }
    }
}
