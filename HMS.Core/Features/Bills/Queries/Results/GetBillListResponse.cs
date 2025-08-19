using HMS.Data.Entities.Enums;

namespace HMS.Core.Features.Bills.Queries.Results
{
    public class GetBillListResponse
    {
        public int BillID { get; set; }
        public int PrescriptionID { get; set; }
        public decimal Amount { get; set; }
        public DateTime BillDate { get; set; }
        public BillStatus Status { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public int DaysOverdue { get; set; }
        public bool IsOverdue { get; set; }
    }
}
