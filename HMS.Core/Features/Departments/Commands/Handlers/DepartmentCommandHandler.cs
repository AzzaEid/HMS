using HMS.Core.Bases;
using HMS.Core.Features.Departments.Commands.Modles;
using HMS.Core.Resources;
using HMS.Data.Entities;
using HMS.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Departments.Commands.Handlers
{

    public class DepartmentCommandHandler : ResponseHandler, IRequestHandler<CreateDepartmentCommand, Response<int>>
                                                       , IRequestHandler<UpdateDepartmentCommand, Response<bool>>
                                                       , IRequestHandler<DeleteDepartmentCommand, Response<bool>>
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentCommandHandler(IDepartmentService departmentService,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _departmentService = departmentService;
        }

        public async Task<Response<int>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = new Department()
            {
                DepartmentName = request.DepartmentName,
                ManagerDoctorId = request.ManagerDoctorId >= 0 ? request.ManagerDoctorId : null,
            };

            var result = await _departmentService.CreateDepartmentAsync(department);
            return Success(result.DepartmentId);
        }

        public async Task<Response<bool>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(request.DepartmentId);
            if (department == null)
            {
                return NotFound<bool>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            }
            department.DepartmentName = request.DepartmentName;
            department.ManagerDoctorId = request.ManagerDoctorId;

            await _departmentService.UpdateDepartmentAsync(department);
            return Success(true);

        }

        public async Task<Response<bool>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var result = await _departmentService.DeleteDepartmentAsync(request.Id);

            if (!result)
                return NotFound<bool>($"Doctor with ID {request.Id} not found.");

            return Success(true);
        }
    }
}


