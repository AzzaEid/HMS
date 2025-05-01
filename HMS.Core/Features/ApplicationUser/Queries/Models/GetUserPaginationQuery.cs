using HMS.Core.Features.ApplicationUser.Queries.Results;
using HMS.Core.Wrappers;
using MediatR;

namespace HMS.Core.Features.ApplicationUser.Queries.Models
{
    public class GetUserPaginationQuery : IRequest<PaginatedResult<GetUserPaginationReponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
