namespace HMS.Data.Entities
{
    public class Patient : Person
    {

        public string Address { get; set; }

        public List<Appointment> Appointments { get; set; }
        public List<Prescription> Prescriptions { get; set; }

    }
}
