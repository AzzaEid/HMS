using HMS.Core.Bases;
using HMS.Data.Results;
using MediatR;

namespace HMS.Core.Features.Authorization.Queries.Models
{
    public class ManageUserClaimsQuery : IRequest<Response<ManageUserClaimsResult>>
    {
        public int UserId { get; set; }
    }
}
