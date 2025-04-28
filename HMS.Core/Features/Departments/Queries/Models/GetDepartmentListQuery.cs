using HMS.Core.Bases;
using HMS.Core.Features.Departments.Queries.Results;
using MediatR;

namespace HMS.Core.Features.Departments.Queries.Models
{

    public class GetDepartmentListQuery : IRequest<Response<List<GetDepartmentListDto>>>
    {
    }
}
