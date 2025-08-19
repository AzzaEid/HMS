using HMS.Data.Abstract;
using HMS.Data.Entities;
using HMS.Data.Helper.Enums;
using HMS.Service.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace HMS.Service.Implementations
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IMedicationRepository _medicationRepository;
        private readonly IBillRepository _billRepository;

        public PrescriptionService(
            IPrescriptionRepository prescriptionRepository,
            IMedicationRepository medicationRepository,
            IBillRepository billRepository)
        {
            _prescriptionRepository = prescriptionRepository;
            _medicationRepository = medicationRepository;
            _billRepository = billRepository;
        }

        public async Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync()
        {
            return await _prescriptionRepository.GetAllPrescriptionsAsync();
        }

        public async Task<Prescription> GetPrescriptionByIdAsync(int id)
        {
            return await _prescriptionRepository.GetPrescriptionByIdAsync(id);
        }

        public async Task<Prescription> CreatePrescriptionAsync(Prescription prescription, List<int> medicationIds)
        {
            // إنشاء الوصفة
            var createdPrescription = await _prescriptionRepository.AddAsync(prescription);

            // إضافة الأدوية للوصفة
            if (medicationIds != null && medicationIds.Any())
            {
                var medications = await _medicationRepository.GetTableNoTracking()
                    .Where(m => medicationIds.Contains(m.MedicationId))
                    .ToListAsync();

                createdPrescription.Medications = medications;
                await _prescriptionRepository.UpdateAsync(createdPrescription);

                // إنشاء فاتورة للوصفة
                await CreateBillForPrescription(createdPrescription);
            }

            return await GetPrescriptionByIdAsync(createdPrescription.Id);
        }

        public async Task<Prescription> UpdatePrescriptionAsync(Prescription prescription, List<int> medicationIds)
        {
            // تحديث الوصفة
            await _prescriptionRepository.UpdateAsync(prescription);

            // تحديث الأدوية
            if (medicationIds != null)
            {
                var medications = await _medicationRepository.GetTableNoTracking()
                    .Where(m => medicationIds.Contains(m.MedicationId))
                    .ToListAsync();

                prescription.Medications = medications;
                await _prescriptionRepository.UpdateAsync(prescription);

                // تحديث الفاتورة
                await UpdateBillForPrescription(prescription);
            }

            return await GetPrescriptionByIdAsync(prescription.Id);
        }

        public async Task<bool> DeletePrescriptionAsync(int id)
        {
            var prescription = await _prescriptionRepository.GetPrescriptionByIdAsync(id);
            if (prescription != null)
            {
                // حذف الفواتير المرتبطة أولاً
                if (prescription.Bills != null && prescription.Bills.Any())
                {
                    await _billRepository.DeleteRangeAsync(prescription.Bills);
                }

                await _prescriptionRepository.DeleteAsync(prescription);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Prescription>> GetPatientPrescriptionsAsync(int patientId)
        {
            return await _prescriptionRepository.GetPatientPrescriptionsAsync(patientId);
        }

        public async Task<IEnumerable<Prescription>> GetDoctorPrescriptionsAsync(int doctorId)
        {
            return await _prescriptionRepository.GetDoctorPrescriptionsAsync(doctorId);
        }

        public IQueryable<Prescription> FilterPrescriptionsPaginatedQueryable(PrescriptionOrderingEnum orderBy, string search = null)
        {
            var query = _prescriptionRepository.GetPrescriptionsQueryable();

            // Apply search if provided
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                query = query.Where(p =>
                    p.Patient.NameEn.ToLower().Contains(search) ||
                    p.Patient.NameAr.ToLower().Contains(search) ||
                    p.Doctor.NameEn.ToLower().Contains(search) ||
                    p.Doctor.NameAr.ToLower().Contains(search) ||
                    p.Medications.Any(m => m.Name.ToLower().Contains(search))
                );
            }

            // Apply ordering
            query = orderBy switch
            {
                PrescriptionOrderingEnum.DateAsc => query.OrderBy(p => p.Date),
                PrescriptionOrderingEnum.DateDesc => query.OrderByDescending(p => p.Date),
                PrescriptionOrderingEnum.PatientNameAsc => query.OrderBy(p => p.Patient.NameEn),
                PrescriptionOrderingEnum.PatientNameDesc => query.OrderByDescending(p => p.Patient.NameEn),
                PrescriptionOrderingEnum.DoctorNameAsc => query.OrderBy(p => p.Doctor.NameEn),
                PrescriptionOrderingEnum.DoctorNameDesc => query.OrderByDescending(p => p.Doctor.NameEn),
                _ => query.OrderByDescending(p => p.Date)
            };

            return query;
        }

        public async Task<List<Prescription>> GetPrescriptionsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _prescriptionRepository.GetPrescriptionsByDateRangeAsync(startDate, endDate);
        }

        private async Task CreateBillForPrescription(Prescription prescription)
        {
            if (prescription.Medications == null || !prescription.Medications.Any())
                return;

            var totalAmount = prescription.Medications.Sum(m => m.Price * m.Quantity);

            var bill = new Bill
            {
                PrescriptionID = prescription.Id,
                Amount = totalAmount,
                BillDate = DateTime.Now,
                Status = HMS.Data.Entities.Enums.BillStatus.Unpaid,
                Prescription = prescription
            };

            await _billRepository.AddAsync(bill);
        }

        private async Task UpdateBillForPrescription(Prescription prescription)
        {
            var existingBill = await _billRepository.GetTableAsTracking()
                .FirstOrDefaultAsync(b => b.PrescriptionID == prescription.Id);

            if (existingBill != null && prescription.Medications != null && prescription.Medications.Any())
            {
                var totalAmount = prescription.Medications.Sum(m => m.Price * m.Quantity);
                existingBill.Amount = totalAmount;
                await _billRepository.UpdateAsync(existingBill);
            }
        }
    }
}
