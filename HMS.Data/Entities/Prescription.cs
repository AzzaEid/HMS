using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Data.Entities
{
    public class Prescription : MedicalRecord
    {

        public List<Medication> Medications { get; set; }
        public List<Bill> Bills { get; set; }
    }

}
