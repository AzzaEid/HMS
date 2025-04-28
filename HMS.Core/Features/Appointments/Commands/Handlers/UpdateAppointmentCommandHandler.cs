using HMS.Core.Bases;
using HMS.Core.Features.Appointments.Commands.Models;
using HMS.Core.Resources;
using HMS.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Appointments.Commands.Handlers
{
    public class UpdateAppointmentCommandHandler : ResponseHandler, IRequestHandler<UpdateAppointmentCommand, Response<bool>>
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;

        public UpdateAppointmentCommandHandler(
            IAppointmentService appointmentService,
            IPatientService patientService,
            IDoctorService doctorService,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _appointmentService = appointmentService;
            _patientService = patientService;
            _doctorService = doctorService;
        }

        public async Task<Response<bool>> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(request.Id);
            if (appointment == null)
                return NotFound<bool>($"Appointment with ID {request.Id} not found.");

            // Validate patient exists
            var patient = await _patientService.GetPatientByIdAsync(request.PatientID);
            if (patient == null)
                return NotFound<bool>($"Patient with ID {request.PatientID} not found.");

            // Validate doctor exists
            var doctor = await _doctorService.GetDoctorByIdAsync(request.DoctorID);
            if (doctor == null)
                return NotFound<bool>($"Doctor with ID {request.DoctorID} not found.");

            appointment.PatientID = request.PatientID;
            appointment.DoctorID = request.DoctorID;
            appointment.Date = request.Date;
            appointment.Status = request.Status;

            await _appointmentService.UpdateAppointmentAsync(appointment);
            return Success(true);
        }
    }

}
