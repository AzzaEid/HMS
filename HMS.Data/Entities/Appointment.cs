using HMS.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Data.Entities
{
    public class Appointment : MedicalRecord
    {

        public AppointmentStatus Status { get; set; }


    }
}
