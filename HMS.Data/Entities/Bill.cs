using HMS.Data.Entities.Enums;

namespace HMS.Data.Entities
{
    public class Bill
    {
        public int BillID { get; set; }
        public int PrescriptionID { get; set; }
        public decimal Amount { get; set; }
        public DateTime BillDate { get; set; } = DateTime.Now;
        public BillStatus Status { get; set; }

        public Prescription Prescription { get; set; }
    }
}
