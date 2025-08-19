using HMS.Core.Bases;
using HMS.Core.Features.Bills.Queries.Results;
using MediatR;

namespace HMS.Core.Features.Bills.Queries.Models
{
    public class GetOverdueBillsQuery : IRequest<Response<List<GetBillListResponse>>>
    {
        public int DaysOverdue { get; set; } = 30;
    }
}
