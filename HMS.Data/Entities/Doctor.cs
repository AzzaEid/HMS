using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Data.Entities.Identity;

namespace HMS.Data.Entities
{
    public class Doctor : User
    {
        [EmailAddress]
        public string Email { get; set; }

        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Prescription> Prescriptions { get; set; }
    }
}
