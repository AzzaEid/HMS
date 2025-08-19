namespace HMS.Core.Features.Medications.Queries.Results
{
    public class GetMedicationListResponse
    {
        public int MedicationId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool IsLowStock { get; set; }
        public int PrescriptionCount { get; set; }
    }
}
