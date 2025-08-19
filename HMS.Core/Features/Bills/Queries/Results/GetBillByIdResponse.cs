using HMS.Data.Entities.Enums;

namespace HMS.Core.Features.Bills.Queries.Results
{
    public class GetBillByIdResponse
    {
        public int BillID { get; set; }
        public int PrescriptionID { get; set; }
        public decimal Amount { get; set; }
        public DateTime BillDate { get; set; }
        public BillStatus Status { get; set; }
        public string PatientName { get; set; }
        public string PatientAddress { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSpecialty { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public List<string> MedicationNames { get; set; } = new List<string>();
        public int DaysOverdue { get; set; }
        public bool IsOverdue { get; set; }
    }
}
