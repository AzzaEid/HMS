using HMS.Core.Bases;
using HMS.Data.Entities.Enums;
using MediatR;

namespace HMS.Core.Features.Appointments.Commands.Models
{
    public class CreateAppointmentCommand : IRequest<Response<int>>
    {
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime Date { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
