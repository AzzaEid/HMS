using HMS.Core.Bases;
using HMS.Core.Features.Appointments.Queries.Results;
using MediatR;

namespace HMS.Core.Features.Appointments.Queries.Models
{
    public class GetPatientAppointmentsQuery : IRequest<Response<List<GetAppointmentListDto>>>
    {
        public int PatientId { get; set; }
    }

}
