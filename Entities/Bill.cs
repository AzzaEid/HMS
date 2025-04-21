using Hospital_Management_System.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Management_System.Entities
{
    public class Bill
    {
        public int BillID { get; set; }
        public int PrescriptionID { get; set; }
        public decimal Amount { get; set; }
        public DateTime BillDate { get; set;} = DateTime.Now;
        public BillStatus Status { get; set; }

        public Prescription Prescription { get; set; }
    }
}
