using HMS.Data.Bases;
using HMS.Data.Entities;
using HMS.Data.Entities.Enums;

namespace HMS.Data.Abstract
{
    public interface IBillRepository : IGenericRepository<Bill>
    {

        Task<IEnumerable<Bill>> GetAllBillsAsync();
        Task<Bill> GetBillByIdAsync(int id);
        IQueryable<Bill> GetBillsQueryable();
        Task<List<Bill>> GetBillsByStatusAsync(BillStatus status);
        Task<List<Bill>> GetPatientBillsAsync(int patientId);
        Task<List<Bill>> GetBillsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalRevenueBetweenDatesAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetUnpaidBillsTotalAsync();
        Task<List<Bill>> GetOverdueBillsAsync(int daysOverdue = 30);
    }
}
