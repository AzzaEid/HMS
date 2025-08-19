using HMS.Core.Bases;
using HMS.Data.Entities.Enums;
using MediatR;

namespace HMS.Core.Features.Bills.Commands.Models
{
    public class UpdateBillCommand : IRequest<Response<string>>
    {
        public int BillID { get; set; }
        public int PrescriptionID { get; set; }
        public decimal Amount { get; set; }
        public DateTime BillDate { get; set; }
        public BillStatus Status { get; set; }
    }
}
