using HMS.Core.Bases;
using HMS.Core.Features.Appointments.Commands.Models;
using HMS.Core.Resources;
using HMS.Data.Entities;
using HMS.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

public class CreateAppointmentCommandHandler : ResponseHandler, IRequestHandler<CreateAppointmentCommand, Response<int>>
{
    private readonly IAppointmentService _appointmentService;
    private readonly IPatientService _patientService;
    private readonly IDoctorService _doctorService;

    public CreateAppointmentCommandHandler(
        IAppointmentService appointmentService,
        IPatientService patientService,
        IDoctorService doctorService,
        IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
    {
        _appointmentService = appointmentService;
        _patientService = patientService;
        _doctorService = doctorService;
    }

    public async Task<Response<int>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        // Validate patient exists
        var patient = await _patientService.GetPatientByIdAsync(request.PatientID);
        if (patient == null)
            return NotFound<int>($"Patient with ID {request.PatientID} not found.");

        // Validate doctor exists
        var doctor = await _doctorService.GetDoctorByIdAsync(request.DoctorID);
        if (doctor == null)
            return NotFound<int>($"Doctor with ID {request.DoctorID} not found.");

        var appointment = new Appointment
        {
            PatientID = request.PatientID,
            DoctorID = request.DoctorID,
            Date = request.Date,
            Status = request.Status
        };

        var result = await _appointmentService.CreateAppointmentAsync(appointment);
        return Success(result.Id);
    }
}
