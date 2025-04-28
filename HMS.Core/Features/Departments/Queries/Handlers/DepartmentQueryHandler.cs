using HMS.Core.Bases;
using HMS.Core.Features.Departments.Queries.Models;
using HMS.Core.Features.Departments.Queries.Results;
using HMS.Core.Features.Doctors.Queries.Results;
using HMS.Core.Resources;
using HMS.Service.Abstracts;
using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Departments.Queries.Handlers
{
    public class DepartmentQueryHandler : ResponseHandler, IRequestHandler<GetDepartmentListQuery, Response<List<GetDepartmentListDto>>>
                                                         , IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentDetailDto>>
                                                         , IRequestHandler<GetDepartmentDoctorsQuery, Response<List<GetDoctorListDto>>>

    {
        private readonly IDepartmentService _departmentService;
        private readonly IDoctorService _doctorService;


        public DepartmentQueryHandler(IDepartmentService departmentService,
                                      IDoctorService doctorService,
                                IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _departmentService = departmentService;
            _doctorService = doctorService;
        }

        public async Task<Response<List<GetDepartmentListDto>>> Handle(GetDepartmentListQuery request, CancellationToken cancellationToken)
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            if (departments == null)
            {
                return NotFound<List<GetDepartmentListDto>>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            }
            var departmentsDto = departments.Adapt<List<GetDepartmentListDto>>();
            return Success(departmentsDto);
        }

        public async Task<Response<GetDepartmentDetailDto>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await _departmentService.GetDepartmentWithDoctorsAsync(request.Id);
            if (department == null)
            {
                return NotFound<GetDepartmentDetailDto>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            }
            var departmentDto = department.Adapt<GetDepartmentDetailDto>();
            return Success(departmentDto);
        }

        public Task<Response<List<GetDoctorListDto>>> Handle(GetDepartmentDoctorsQuery request, CancellationToken cancellationToken)
        {
            var doctors = _doctorService.GetDoctorsOfDepartmentAsync(request.Id);
            if (doctors == null)
            {
                return Task.FromResult(NotFound<List<GetDoctorListDto>>("There is no doctors in this dapartment"));

            }
            return Task.FromResult(Success(doctors.Adapt<List<GetDoctorListDto>>()));
        }
    }
}

