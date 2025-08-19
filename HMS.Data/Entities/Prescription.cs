namespace HMS.Data.Entities
{
    public class Prescription : MedicalRecord
    {

        public List<Medication> Medications { get; set; }
        public List<Bill> Bills { get; set; }
    }

}
