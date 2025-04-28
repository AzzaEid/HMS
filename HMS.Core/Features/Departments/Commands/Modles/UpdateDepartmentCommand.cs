using HMS.Core.Bases;
using MediatR;

namespace HMS.Core.Features.Departments.Commands.Modles
{
    public class UpdateDepartmentCommand : IRequest<Response<bool>>
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int ManagerDoctorId { get; set; }
    }
}
