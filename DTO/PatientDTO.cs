using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management_System.Entities
{
    internal class PatientDTO : Person
    {

        public string Address { get; set; }


    }
}
