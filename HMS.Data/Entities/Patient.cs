using HMS.Data.Entities.Identity;

namespace HMS.Data.Entities
{
    public class Patient : User
    {

        public string Address { get; set; }

        public List<Appointment> Appointments { get; set; }
        public List<Prescription> Prescriptions { get; set; }

    }
}
