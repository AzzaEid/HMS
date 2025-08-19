using HMS.Core.Bases;
using MediatR;

namespace HMS.Core.Features.Bills.Commands.Models
{
    public class PayBillCommand : IRequest<Response<string>>
    {
        public int BillID { get; set; }
    }
}
