using HMS.Core.Bases;
using HMS.Core.Features.Authorization.Queries.Results;
using MediatR;

namespace HMS.Core.Features.Authorization.Queries.Models
{
    public class GetRoleByIdQuery : IRequest<Response<GetRoleByIdResult>>
    {
        public int Id { get; set; }
    }
}
