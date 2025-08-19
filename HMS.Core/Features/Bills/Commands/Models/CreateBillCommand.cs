using HMS.Core.Bases;
using HMS.Data.Entities.Enums;
using MediatR;

namespace HMS.Core.Features.Bills.Commands.Models
{
    public class CreateBillCommand : IRequest<Response<string>>
    {
        public int PrescriptionID { get; set; }
        public decimal Amount { get; set; }
        public DateTime BillDate { get; set; } = DateTime.Now;
        public BillStatus Status { get; set; } = BillStatus.Unpaid;
    }
}
