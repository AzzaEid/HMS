namespace HMS.Core.Features.Prescriptions.Queries.Results
{
    public class GetPrescriptionListResponse
    {
        public int Id { get; set; }
        public int PatientID { get; set; }
        public string PatientNameEn { get; set; }
        public string PatientNameAr { get; set; }
        public int DoctorID { get; set; }
        public string DoctorNameEn { get; set; }
        public string DoctorNameAr { get; set; }
        public DateTime Date { get; set; }
        public List<string> MedicationNames { get; set; } = new List<string>();
        public decimal TotalAmount { get; set; }
        public bool HasUnpaidBills { get; set; }
    }
}
