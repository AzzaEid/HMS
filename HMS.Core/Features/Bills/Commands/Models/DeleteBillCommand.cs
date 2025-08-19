using HMS.Core.Bases;
using MediatR;

namespace HMS.Core.Features.Bills.Commands.Models
{
    public class DeleteBillCommand : IRequest<Response<string>>
    {
        public int BillID { get; set; }
    }
}
