using HMS.Data.Abstract;
using HMS.Data.Entities;
using HMS.Data.Entities.Enums;
using HMS.Infrustructure.Bases;
using HMS.Infrustructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrustructure.Repository
{
    public class BillRepository : GenericRepository<Bill>, IBillRepository
    {
        #region Fields 
        private readonly DbSet<Bill> _bills;
        #endregion

        #region Constructor
        public BillRepository(ApplicationDBContext dbcontext) : base(dbcontext)
        {
            _bills = dbcontext.Set<Bill>();
        }
        #endregion

        public async Task<IEnumerable<Bill>> GetAllBillsAsync()
        {
            return await _bills
                .Include(b => b.Prescription)
                    .ThenInclude(p => p.Patient)
                .Include(b => b.Prescription)
                    .ThenInclude(p => p.Doctor)
                .ToListAsync();
        }

        public async Task<Bill> GetBillByIdAsync(int id)
        {
            return await _bills
                .Include(b => b.Prescription)
                    .ThenInclude(p => p.Patient)
                .Include(b => b.Prescription)
                    .ThenInclude(p => p.Doctor)
                .Include(b => b.Prescription)
                    .ThenInclude(p => p.Medications)
                .FirstOrDefaultAsync(b => b.BillID == id);
        }

        public IQueryable<Bill> GetBillsQueryable()
        {
            return _bills
                .Include(b => b.Prescription)
                    .ThenInclude(p => p.Patient)
                .Include(b => b.Prescription)
                    .ThenInclude(p => p.Doctor)
                .AsQueryable();
        }

        public async Task<List<Bill>> GetBillsByStatusAsync(BillStatus status)
        {
            return await _bills
                .Include(b => b.Prescription)
                    .ThenInclude(p => p.Patient)
                .Include(b => b.Prescription)
                    .ThenInclude(p => p.Doctor)
                .Where(b => b.Status == status)
                .OrderByDescending(b => b.BillDate)
                .ToListAsync();
        }

        public async Task<List<Bill>> GetPatientBillsAsync(int patientId)
        {
            return await _bills
                .Include(b => b.Prescription)
                    .ThenInclude(p => p.Doctor)
                .Where(b => b.Prescription.PatientID == patientId)
                .OrderByDescending(b => b.BillDate)
                .ToListAsync();
        }

        public async Task<List<Bill>> GetBillsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _bills
                .Include(b => b.Prescription)
                    .ThenInclude(p => p.Patient)
                .Include(b => b.Prescription)
                    .ThenInclude(p => p.Doctor)
                .Where(b => b.BillDate >= startDate && b.BillDate <= endDate)
                .OrderByDescending(b => b.BillDate)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalRevenueBetweenDatesAsync(DateTime startDate, DateTime endDate)
        {
            return await _bills
                .Where(b => b.Status == BillStatus.Paid &&
                           b.BillDate >= startDate &&
                           b.BillDate <= endDate)
                .SumAsync(b => b.Amount);
        }

        public async Task<decimal> GetUnpaidBillsTotalAsync()
        {
            return await _bills
                .Where(b => b.Status == BillStatus.Unpaid)
                .SumAsync(b => b.Amount);
        }

        public async Task<List<Bill>> GetOverdueBillsAsync(int daysOverdue = 30)
        {
            var overdueDate = DateTime.Now.AddDays(-daysOverdue);
            return await _bills
                .Include(b => b.Prescription)
                    .ThenInclude(p => p.Patient)
                .Where(b => b.Status == BillStatus.Unpaid && b.BillDate <= overdueDate)
                .OrderBy(b => b.BillDate)
                .ToListAsync();
        }
    }
}
