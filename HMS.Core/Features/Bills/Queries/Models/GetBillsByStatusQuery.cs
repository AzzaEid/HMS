using HMS.Core.Bases;
using HMS.Core.Features.Bills.Queries.Results;
using HMS.Data.Entities.Enums;
using MediatR;

namespace HMS.Core.Features.Bills.Queries.Models
{
    public class GetBillsByStatusQuery : IRequest<Response<List<GetBillListResponse>>>
    {
        public BillStatus Status { get; set; }
    }
}
