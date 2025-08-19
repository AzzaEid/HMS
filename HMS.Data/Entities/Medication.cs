namespace HMS.Data.Entities
{
    public class Medication
    {
        public int MedicationId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public List<Prescription> Prescriptions { get; set; }
    }
}
