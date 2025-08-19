using HMS.Data.Entities;
using HMS.Data.Entities.Enums;
using HMS.Data.Helper.Enums;
using HMS.Service.Implementations;

namespace HMS.Service.Abstracts
{
    public interface IBillService
    {
        Task<IEnumerable<Bill>> GetAllBillsAsync();
        Task<Bill> GetBillByIdAsync(int id);
        Task<Bill> CreateBillAsync(Bill bill);
        Task<Bill> UpdateBillAsync(Bill bill);
        Task<bool> DeleteBillAsync(int id);
        Task<bool> PayBillAsync(int billId);
        Task<List<Bill>> GetBillsByStatusAsync(BillStatus status);
        Task<List<Bill>> GetPatientBillsAsync(int patientId);
        Task<List<Bill>> GetBillsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalRevenueBetweenDatesAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetUnpaidBillsTotalAsync();
        Task<List<Bill>> GetOverdueBillsAsync(int daysOverdue = 30);
        IQueryable<Bill> FilterBillsPaginatedQueryable(BillOrderingEnum orderBy, string search = null);
        Task<BillStatistics> GetBillStatisticsAsync();
    }
}
