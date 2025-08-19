namespace HMS.Core.Features.Prescriptions.Queries.Results
{
    public class MedicationInPrescriptionResponse
    {
        public int MedicationId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
