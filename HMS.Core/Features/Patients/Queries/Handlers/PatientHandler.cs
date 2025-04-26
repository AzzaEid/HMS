using Azure;
using HMS.Core.Bases;
using HMS.Core.Features.Patients.Queries.Models;
using HMS.Core.Features.Patients.Queries.Results;
using HMS.Data.Entities;
using HMS.Service.Abstracts;
using HMS.Service.Implementations;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Core.Bases;

namespace HMS.Core.Features.Patients.Queries.Handlers
{
    public class PatientHandler : ResponseHandler, IRequestHandler<GetPatientListQuery, Bases.Response<List<GetPatientListDto>>>
    {
        private readonly IPatientService _patientService;

        public PatientHandler( IPatientService patientService) 
        {
            _patientService = patientService;
        }

        public async Task<Bases.Response<List<GetPatientListDto>>> Handle(GetPatientListQuery request, CancellationToken cancellationToken)
        {
            var patients = await _patientService.GetAllPatientsAsync();
            var patientsListMapper = patients.Adapt<List<GetPatientListDto>>();
            return Success(patientsListMapper);
        }
    }
}
