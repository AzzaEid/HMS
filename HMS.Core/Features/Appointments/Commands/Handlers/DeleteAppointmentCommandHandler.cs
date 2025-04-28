using HMS.Core.Bases;
using HMS.Core.Features.Appointments.Commands.Models;
using HMS.Core.Resources;
using HMS.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Appointments.Commands.Handlers
{
    public class DeleteAppointmentCommandHandler : ResponseHandler, IRequestHandler<DeleteAppointmentCommand, Response<bool>>
    {
        private readonly IAppointmentService _appointmentService;

        public DeleteAppointmentCommandHandler(IAppointmentService appointmentService,
                                                IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _appointmentService = appointmentService;
        }

        public async Task<Response<bool>> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(request.Id);

            if (appointment == null)
                return NotFound<bool>($"Appointment with ID {request.Id} not found.");

            await _appointmentService.DeleteAppointmentAsync(request.Id);
            return Success(true);
        }
    }
}
