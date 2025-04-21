using Hospital_Management_System.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management_System.Entities
{
    internal class BillDTO
    {
        public int PrescriptionID { get; set; }
        public decimal Amount { get; set; }
        public BillStatus Status { get; set; }

    }
}
