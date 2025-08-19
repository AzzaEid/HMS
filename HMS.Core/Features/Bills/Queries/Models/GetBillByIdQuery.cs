using HMS.Core.Bases;
using HMS.Core.Features.Bills.Queries.Results;
using MediatR;

namespace HMS.Core.Features.Bills.Queries.Models
{
    public class GetBillByIdQuery : IRequest<Response<GetBillByIdResponse>>
    {
        public int BillID { get; set; }
    }
}
