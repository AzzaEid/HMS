using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital_Management_System.DTO;
using Hospital_Management_System.Entities.Enums;

namespace Hospital_Management_System.Entities
{
    internal class AppointmentDTO : MedicalRecordDTO
    {
        public AppointmentStatus Status { get; set; }

    }
}
