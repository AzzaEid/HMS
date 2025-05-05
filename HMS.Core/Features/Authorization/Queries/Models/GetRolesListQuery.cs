using HMS.Core.Bases;
using HMS.Core.Features.Authorization.Queries.Results;
using MediatR;

namespace HMS.Core.Features.Authorization.Queries.Models
{
    public class GetRolesListQuery : IRequest<Response<List<GetRolesListResult>>>
    {
    }
}