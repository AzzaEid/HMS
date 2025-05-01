using HMS.Core.Bases;
using HMS.Core.Features.Patients.Queries.Models;
using HMS.Core.Features.Patients.Queries.Results;
using HMS.Core.Resources;
using HMS.Core.Wrappers;
using HMS.Data.Entities;
using HMS.Service.Abstracts;
using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;

namespace HMS.Core.Features.Patients.Queries.Handlers
{
    public class PatientHandler : ResponseHandler, IRequestHandler<GetPatientListQuery, Response<List<GetPatientListDto>>>,
                                                   IRequestHandler<GetPatientByIdQuery, Response<GetPatientDetailDto>>,
                                                   IRequestHandler<GetPatientPaginatedListQuery, PaginatedResult<GetPatientPaginatedListResponse>>

    {
        private readonly IPatientService _patientService;
        public PatientHandler(IPatientService patientService,
                               IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _patientService = patientService;
        }

        public async Task<Response<List<GetPatientListDto>>> Handle(GetPatientListQuery request, CancellationToken cancellationToken)
        {
            var patients = await _patientService.GetAllPatientsAsync();
            var patientsListMapper = patients.Adapt<List<GetPatientListDto>>();
            var result = Success(patientsListMapper);
            result.Meta = new { Count = patientsListMapper.Count() };
            return result;
        }

        public async Task<Response<GetPatientDetailDto>> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var patient = await _patientService.GetPatientByIdAsync(request.Id);

            if (patient == null)
                return NotFound<GetPatientDetailDto>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            var patientDto = patient.Adapt<GetPatientDetailDto>();
            return Success(patientDto);
        }

        public async Task<PaginatedResult<GetPatientPaginatedListResponse>> Handle(GetPatientPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Patient, GetPatientPaginatedListResponse>> expression = p => new GetPatientPaginatedListResponse(p.Id, p.Localize(p.NameAr, p.NameEn), p.Address);
            //var querable = _patientService.GetAllPatientsQueryable();
            var filter = _patientService.FilterPatientPaginatedQuerable(request.OrderBy, request.Search);
            var pagedList = await filter.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return pagedList;

        }
    }

}

