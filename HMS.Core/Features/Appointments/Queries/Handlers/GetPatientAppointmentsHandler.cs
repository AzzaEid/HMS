using HMS.Core.Bases;
using HMS.Core.Features.Appointments.Queries.Models;
using HMS.Core.Features.Appointments.Queries.Results;
using HMS.Core.Resources;
using HMS.Service.Abstracts;
using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Appointments.Queries.Handlers
{
    public class GetPatientAppointmentsHandler : ResponseHandler, IRequestHandler<GetPatientAppointmentsQuery, Response<List<GetAppointmentListDto>>>
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;

        public GetPatientAppointmentsHandler(
            IAppointmentService appointmentService,
            IPatientService patientService,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _appointmentService = appointmentService;
            _patientService = patientService;
        }

        public async Task<Response<List<GetAppointmentListDto>>> Handle(GetPatientAppointmentsQuery request, CancellationToken cancellationToken)
        {
            // Check if patient exists
            var patient = await _patientService.GetPatientByIdAsync(request.PatientId);
            if (patient == null)
                return NotFound<List<GetAppointmentListDto>>($"Patient with ID {request.PatientId} not found.");

            var appointments = await _appointmentService.GetPatientAppointmentsAsync(request.PatientId);
            var appointmentsDto = appointments.Adapt<List<GetAppointmentListDto>>();

            // Set custom mapping properties if needed
            for (int i = 0; i < appointments.Count; i++)
            {
                appointmentsDto[i].PatientName = patient.Localize(patient.NameAr, patient.NameEn);
                appointmentsDto[i].DoctorName = appointments[i].Doctor.Localize(appointments[i].Doctor.NameAr, appointments[i].Doctor.NameEn);
            }

            return Success(appointmentsDto);
        }
    }

}

