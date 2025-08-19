namespace HMS.Core.Features.Medications.Queries.Results
{
    public class GetMedicationByIdResponse
    {
        public int MedicationId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool IsLowStock { get; set; }
        public List<PrescriptionInMedicationResponse> Prescriptions { get; set; } = new List<PrescriptionInMedicationResponse>();
    }

    public class PrescriptionInMedicationResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
    }
}
