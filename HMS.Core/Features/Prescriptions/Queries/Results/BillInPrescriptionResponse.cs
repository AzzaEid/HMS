using HMS.Data.Entities.Enums;

namespace HMS.Core.Features.Prescriptions.Queries.Results
{
    public class BillInPrescriptionResponse
    {
        public int BillID { get; set; }
        public decimal Amount { get; set; }
        public DateTime BillDate { get; set; }
        public BillStatus Status { get; set; }
    }
}
