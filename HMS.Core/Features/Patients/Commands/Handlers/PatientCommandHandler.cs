using HMS.Core.Bases;
using HMS.Core.Features.Patients.Commands.Modles;
using HMS.Core.Resources;
using HMS.Data.Entities;
using HMS.Service.Abstracts;
using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Patients.Commands.Handlers
{
    public class PatientCommandHandler : ResponseHandler, IRequestHandler<CreatePatientCommand, Response<int>>,
                                                          IRequestHandler<DeletePatientCommand, Response<bool>>,
                                                          IRequestHandler<UpdatePatientCommand, Response<bool>>
    {
        private readonly IPatientService _patientService;

        public PatientCommandHandler(IPatientService patientService,
                                     IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _patientService = patientService;
        }

        public async Task<Response<int>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = request.Adapt<Patient>();

            var result = await _patientService.CreatePatientAsync(patient);
            return Created(result.Id);
        }

        public async Task<Response<bool>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await _patientService.GetPatientByIdAsync(request.Id);

            if (patient == null)
                return NotFound<bool>($"Patient with ID {request.Id} not found.");

            patient.NameEn = request.NameEn;
            patient.NameAr = request.NameAr;
            patient.Address = request.Address;

            // we can just use this: request.Adapt(patient);
            await _patientService.UpdatePatientAsync(patient);
            return Success(true);
        }

        public async Task<Response<bool>> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await _patientService.GetPatientByIdAsync(request.Id);

            if (patient == null)
                return NotFound<bool>($"Patient with ID {request.Id} not found.");

            await _patientService.DeletePatientAsync(request.Id);
            return Success(true);
        }
    }
}
