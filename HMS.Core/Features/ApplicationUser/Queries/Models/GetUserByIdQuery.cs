using HMS.Core.Bases;
using HMS.Core.Features.ApplicationUser.Queries.Results;
using MediatR;

namespace HMS.Core.Features.ApplicationUser.Queries.Models
{
    public class GetUserByIdQuery : IRequest<Response<GetUserByIdResponse>>
    {
        public int Id { get; set; }
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
