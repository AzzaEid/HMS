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
    public class GetAppointmentByIdHandler : ResponseHandler, IRequestHandler<GetAppointmentByIdQuery, Response<GetAppointmentDetailDto>>
    {
        private readonly IAppointmentService _appointmentService;

        public GetAppointmentByIdHandler(IAppointmentService appointmentService, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _appointmentService = appointmentService;
        }

        public async Task<Response<GetAppointmentDetailDto>> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(request.Id);

            if (appointment == null)
                return NotFound<GetAppointmentDetailDto>($"Appointment with ID {request.Id} not found.");

            var appointmentDto = appointment.Adapt<GetAppointmentDetailDto>();

            // Set additional properties that might need custom mapping
            appointmentDto.PatientName = appointment.Patient.Localize(appointment.Patient.NameAr, appointment.Patient.NameEn);
            appointmentDto.DoctorName = appointment.Doctor.Localize(appointment.Doctor.NameAr, appointment.Doctor.NameEn);
            appointmentDto.DoctorSpecialty = appointment.Doctor.Specialty?.SpecialtyName;

            return Success(appointmentDto);
        }
    }

}
