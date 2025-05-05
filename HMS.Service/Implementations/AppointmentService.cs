using HMS.Data.Entities;
using HMS.Data.Helper.Enums;
using HMS.Infrustructure.Abstract;
using HMS.Service.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace HMS.Service.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public IQueryable<Appointment> GetAllAppointmentsQueryable()
        {
            return _appointmentRepository.GetTableNoTracking()
                .Include(a => a.Patient)
                .Include(a => a.Doctor);
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            return await _appointmentRepository.GetAppointmentByIdAsync(id);
        }

        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            return await _appointmentRepository.AddAsync(appointment);
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            await _appointmentRepository.UpdateAsync(appointment);
        }

        public async Task DeleteAppointmentAsync(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment != null)
            {
                await _appointmentRepository.DeleteAsync(appointment);
            }
        }

        public async Task<List<Appointment>> GetPatientAppointmentsAsync(int patientId)
        {
            return await _appointmentRepository.GetPatientAppointmentsAsync(patientId);
        }

        public async Task<List<Appointment>> GetDoctorAppointmentsAsync(int doctorId)
        {
            return await _appointmentRepository.GetDoctorAppointmentsAsync(doctorId);
        }

        public IQueryable<Appointment> FilterAppointmentPaginatedQuerable(AppointmentOrderingEnum orderBy, string search = null)
        {
            var query = GetAllAppointmentsQueryable();

            // Apply search if provided
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                query = query.Where(a =>
                    a.Patient.NameEn.ToLower().Contains(search) ||
                    a.Doctor.NameEn.ToLower().Contains(search) ||
                    a.Patient.NameAr.ToLower().Contains(search) ||
                    a.Doctor.NameAr.ToLower().Contains(search) ||
                    a.Status.ToString().ToLower().Contains(search)
                );
            }

            // Apply ordering
            query = orderBy switch
            {
                AppointmentOrderingEnum.DateAsc => query.OrderBy(a => a.Date),
                AppointmentOrderingEnum.DateDesc => query.OrderByDescending(a => a.Date),
                AppointmentOrderingEnum.StatusAsc => query.OrderBy(a => a.Status),
                AppointmentOrderingEnum.StatusDesc => query.OrderByDescending(a => a.Status),
                AppointmentOrderingEnum.PatientNameAsc => query.OrderBy(a => a.Patient.NameEn),
                AppointmentOrderingEnum.PatientNameDesc => query.OrderByDescending(a => a.Patient.NameEn),
                AppointmentOrderingEnum.DoctorNameAsc => query.OrderBy(a => a.Doctor.NameEn),
                AppointmentOrderingEnum.DoctorNameDesc => query.OrderByDescending(a => a.Doctor.NameEn),
                _ => query.OrderByDescending(a => a.Date)
            };

            return query;
        }
    }
}
