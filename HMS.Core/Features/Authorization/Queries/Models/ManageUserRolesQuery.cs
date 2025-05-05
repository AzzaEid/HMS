using HMS.Core.Bases;
using HMS.Data.Results;
using MediatR;

namespace HMS.Core.Features.Authorization.Queries.Models
{
    public class ManageUserRolesQuery : IRequest<Response<ManageUserRolesResult>>
    {
        public int UserId { get; set; }
    }
}
