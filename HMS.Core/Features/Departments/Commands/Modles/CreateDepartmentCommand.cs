using HMS.Core.Bases;
using MediatR;

namespace HMS.Core.Features.Departments.Commands.Modles
{
    public class CreateDepartmentCommand : IRequest<Response<int>>
    {
        public string DepartmentName { get; set; }
        public int? ManagerDoctorId { get; set; }
    }
}
