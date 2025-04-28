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
    public class GetDoctorAppointmentsHandler : ResponseHandler, IRequestHandler<GetDoctorAppointmentsQuery, Response<List<GetAppointmentListDto>>>
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;

        public GetDoctorAppointmentsHandler(
            IAppointmentService appointmentService,
            IDoctorService doctorService,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _appointmentService = appointmentService;
            _doctorService = doctorService;
        }

        public async Task<Response<List<GetAppointmentListDto>>> Handle(GetDoctorAppointmentsQuery request, CancellationToken cancellationToken)
        {
            // Check if doctor exists
            var doctor = await _doctorService.GetDoctorByIdAsync(request.DoctorId);
            if (doctor == null)
                return NotFound<List<GetAppointmentListDto>>($"Doctor with ID {request.DoctorId} not found.");

            var appointments = await _appointmentService.GetDoctorAppointmentsAsync(request.DoctorId);
            var appointmentsDto = appointments.Adapt<List<GetAppointmentListDto>>();

            // Set custom mapping properties if needed
            for (int i = 0; i < appointments.Count; i++)
            {
                appointmentsDto[i].DoctorName = doctor.Localize(doctor.NameAr, doctor.NameEn);
                appointmentsDto[i].PatientName = appointments[i].Patient.Localize(appointments[i].Patient.NameAr, appointments[i].Patient.NameEn);
            }

            return Success(appointmentsDto);
        }
    }
}
