using HMS.Core.Bases;
using HMS.Data.Entities.Enums;
using MediatR;

namespace HMS.Core.Features.Appointments.Commands.Models
{
    public class UpdateAppointmentCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime Date { get; set; }
        public AppointmentStatus Status { get; set; }
    }

}
