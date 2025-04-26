using HMS.Core.Bases;
using HMS.Core.Features.Patients.Commands.Modles;
using HMS.Data.Entities;
using HMS.Service.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Core.Features.Patients.Commands.Handlers
{
    public class CreatePatientCommandHandler : ResponseHandler, IRequestHandler<CreatePatientCommand, Response<int>>
    {
        private readonly IPatientService _patientService;

        public CreatePatientCommandHandler(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<Response<int>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = new Patient
            {
                Name = request.Name,
                Age = request.Age,
                Gender = request.Gender,
                ContactNumber = request.ContactNumber,
                Address = request.Address
            };

            var result = await _patientService.CreatePatientAsync(patient);
            return Created(result.Id);
        }
    }
}
