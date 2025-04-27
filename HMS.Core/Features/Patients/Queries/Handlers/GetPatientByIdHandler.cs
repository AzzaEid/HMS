using HMS.Core.Bases;
using HMS.Core.Features.Patients.Queries.Models;
using HMS.Core.Features.Patients.Queries.Results;
using HMS.Service.Abstracts;
using Mapster;
using MediatR;

namespace HMS.Core.Features.Patients.Queries.Handlers
{
    public class GetPatientByIdHandler : ResponseHandler, IRequestHandler<GetPatientByIdQuery, Response<GetPatientDetailDto>>
    {
        private readonly IPatientService _patientService;

        public GetPatientByIdHandler(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<Response<GetPatientDetailDto>> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var patient = await _patientService.GetPatientByIdAsync(request.Id);

            if (patient == null)
                return NotFound<GetPatientDetailDto>($"Patient with ID {request.Id} not found.");

            var patientDto = patient.Adapt<GetPatientDetailDto>();
            return Success(patientDto);
        }
    }

}

