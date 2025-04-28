using HMS.Core.Bases;
using MediatR;

namespace HMS.Core.Features.Appointments.Commands.Models
{
    public class DeleteAppointmentCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
    }
}
