namespace HMS.Core.Features.Prescriptions.Queries.Results
{
    public class GetPrescriptionByIdResponse
    {
        public int Id { get; set; }
        public int PatientID { get; set; }
        public string PatientNameEn { get; set; }
        public string PatientNameAr { get; set; }
        public string PatientAddress { get; set; }
        public int DoctorID { get; set; }
        public string DoctorNameEn { get; set; }
        public string DoctorNameAr { get; set; }
        public string DoctorSpecialty { get; set; }
        public DateTime Date { get; set; }
        public List<MedicationInPrescriptionResponse> Medications { get; set; } = new List<MedicationInPrescriptionResponse>();
        public List<BillInPrescriptionResponse> Bills { get; set; } = new List<BillInPrescriptionResponse>();
        public decimal TotalAmount { get; set; }
    }
}
