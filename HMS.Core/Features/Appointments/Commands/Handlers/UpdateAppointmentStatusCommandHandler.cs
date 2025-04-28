using HMS.Core.Bases;
using HMS.Core.Features.Appointments.Commands.Models;
using HMS.Core.Resources;
using HMS.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Appointments.Commands.Handlers
{
    public class UpdateAppointmentStatusCommandHandler : ResponseHandler, IRequestHandler<UpdateAppointmentStatusCommand, Response<bool>>
    {
        private readonly IAppointmentService _appointmentService;

        public UpdateAppointmentStatusCommandHandler(IAppointmentService appointmentService,
                                                      IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _appointmentService = appointmentService;
        }

        public async Task<Response<bool>> Handle(UpdateAppointmentStatusCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(request.Id);
            if (appointment == null)
                return NotFound<bool>($"Appointment with ID {request.Id} not found.");

            appointment.Status = request.Status;

            await _appointmentService.UpdateAppointmentAsync(appointment);
            return Success(true);
        }
    }
}
