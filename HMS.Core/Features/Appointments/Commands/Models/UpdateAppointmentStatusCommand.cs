using HMS.Core.Bases;
using HMS.Data.Entities.Enums;
using MediatR;

namespace HMS.Core.Features.Appointments.Commands.Models
{
    public class UpdateAppointmentStatusCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public AppointmentStatus Status { get; set; }
    }

}
