using HMS.Core.Bases;
using MediatR;

namespace HMS.Core.Features.Departments.Commands.Modles
{
    public class DeleteDepartmentCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
    }
}
