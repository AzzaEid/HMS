using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management_System.Entities
{
    public class Doctor : Person
    {
        [EmailAddress]
        public string Email { get; set; }

        public int SpecialtyId {  get; set; }
        public Specialty Specialty { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Prescription> Prescriptions { get; set; }
    }
}
