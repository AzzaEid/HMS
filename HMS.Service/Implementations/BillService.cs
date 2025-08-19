using HMS.Data.Abstract;
using HMS.Data.Entities;
using HMS.Data.Entities.Enums;
using HMS.Data.Helper.Enums;
using HMS.Service.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace HMS.Service.Implementations
{
    public class BillService : IBillService
    {
        private readonly IBillRepository _billRepository;
        private readonly IPrescriptionRepository _prescriptionRepository;

        public BillService(
            IBillRepository billRepository,
            IPrescriptionRepository prescriptionRepository)
        {
            _billRepository = billRepository;
            _prescriptionRepository = prescriptionRepository;
        }

        public async Task<IEnumerable<Bill>> GetAllBillsAsync()
        {
            return await _billRepository.GetAllBillsAsync();
        }

        public async Task<Bill> GetBillByIdAsync(int id)
        {
            return await _billRepository.GetBillByIdAsync(id);
        }

        public async Task<Bill> CreateBillAsync(Bill bill)
        {
            return await _billRepository.AddAsync(bill);
        }

        public async Task<Bill> UpdateBillAsync(Bill bill)
        {
            await _billRepository.UpdateAsync(bill);
            return await GetBillByIdAsync(bill.BillID);
        }

        public async Task<bool> DeleteBillAsync(int id)
        {
            var bill = await _billRepository.GetBillByIdAsync(id);
            if (bill != null)
            {
                // لا يمكن حذف فاتورة مدفوعة
                if (bill.Status == BillStatus.Paid)
                    return false;

                await _billRepository.DeleteAsync(bill);
                return true;
            }
            return false;
        }

        public async Task<bool> PayBillAsync(int billId)
        {
            var bill = await _billRepository.GetBillByIdAsync(billId);
            if (bill != null && bill.Status == BillStatus.Unpaid)
            {
                bill.Status = BillStatus.Paid;
                await _billRepository.UpdateAsync(bill);
                return true;
            }
            return false;
        }

        public async Task<List<Bill>> GetBillsByStatusAsync(BillStatus status)
        {
            return await _billRepository.GetBillsByStatusAsync(status);
        }

        public async Task<List<Bill>> GetPatientBillsAsync(int patientId)
        {
            return await _billRepository.GetPatientBillsAsync(patientId);
        }

        public async Task<List<Bill>> GetBillsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _billRepository.GetBillsByDateRangeAsync(startDate, endDate);
        }

        public async Task<decimal> GetTotalRevenueBetweenDatesAsync(DateTime startDate, DateTime endDate)
        {
            return await _billRepository.GetTotalRevenueBetweenDatesAsync(startDate, endDate);
        }

        public async Task<decimal> GetUnpaidBillsTotalAsync()
        {
            return await _billRepository.GetUnpaidBillsTotalAsync();
        }

        public async Task<List<Bill>> GetOverdueBillsAsync(int daysOverdue = 30)
        {
            return await _billRepository.GetOverdueBillsAsync(daysOverdue);
        }

        public IQueryable<Bill> FilterBillsPaginatedQueryable(BillOrderingEnum orderBy, string search = null)
        {
            var query = _billRepository.GetBillsQueryable();

            // Apply search if provided
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                query = query.Where(b =>
                    b.Prescription.Patient.NameEn.ToLower().Contains(search) ||
                    b.Prescription.Patient.NameAr.ToLower().Contains(search) ||
                    b.Prescription.Doctor.NameEn.ToLower().Contains(search) ||
                    b.Prescription.Doctor.NameAr.ToLower().Contains(search) ||
                    b.Status.ToString().ToLower().Contains(search) ||
                    b.BillID.ToString().Contains(search)
                );
            }

            // Apply ordering
            query = orderBy switch
            {
                BillOrderingEnum.DateAsc => query.OrderBy(b => b.BillDate),
                BillOrderingEnum.DateDesc => query.OrderByDescending(b => b.BillDate),
                BillOrderingEnum.AmountAsc => query.OrderBy(b => b.Amount),
                BillOrderingEnum.AmountDesc => query.OrderByDescending(b => b.Amount),
                BillOrderingEnum.StatusAsc => query.OrderBy(b => b.Status),
                BillOrderingEnum.StatusDesc => query.OrderByDescending(b => b.Status),
                BillOrderingEnum.PatientNameAsc => query.OrderBy(b => b.Prescription.Patient.NameEn),
                BillOrderingEnum.PatientNameDesc => query.OrderByDescending(b => b.Prescription.Patient.NameEn),
                _ => query.OrderByDescending(b => b.BillDate)
            };

            return query;
        }

        public async Task<BillStatistics> GetBillStatisticsAsync()
        {
            var totalBills = await _billRepository.GetTableNoTracking().CountAsync();
            var paidBills = await _billRepository.GetTableNoTracking().CountAsync(b => b.Status == BillStatus.Paid);
            var pendingBills = await _billRepository.GetTableNoTracking().CountAsync(b => b.Status == BillStatus.Unpaid);
            var totalRevenue = await _billRepository.GetTableNoTracking()
                .Where(b => b.Status == BillStatus.Paid)
                .SumAsync(b => b.Amount);
            var unpaidAmount = await GetUnpaidBillsTotalAsync();

            return new BillStatistics
            {
                TotalBills = totalBills,
                PaidBills = paidBills,
                PendingBills = pendingBills,
                TotalRevenue = totalRevenue,
                UnpaidAmount = unpaidAmount,
                PaymentRate = totalBills > 0 ? (double)paidBills / totalBills * 100 : 0
            };
        }
    }

    public class BillStatistics
    {
        public int TotalBills { get; set; }
        public int PaidBills { get; set; }
        public int PendingBills { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal UnpaidAmount { get; set; }
        public double PaymentRate { get; set; }
    }
}
