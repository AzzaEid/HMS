using HMS.Data.Entities;
using HMS.Data.Helper;
using HMS.Infrustructure.Abstract;
using HMS.Service.Abstracts;

namespace HMS.Service.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _patientRepository.GetAllPatientsAsync();
        }
        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            return await _patientRepository.GetByIdAsync(id);
        }

        public async Task<Patient> CreatePatientAsync(Patient patient)
        {
            return await _patientRepository.AddAsync(patient);
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            await _patientRepository.UpdateAsync(patient);
        }

        public async Task DeletePatientAsync(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient != null)
            {
                await _patientRepository.DeleteAsync(patient);
            }
        }

        public IQueryable<Patient> GetAllPatientsQueryable()
        {
            return _patientRepository.GetTableNoTracking().AsQueryable();
        }

        public IQueryable<Patient> FilterPatientPaginatedQuerable(PatientOrderingEnum order, string? search)
        {
            var queryble = _patientRepository.GetTableNoTracking().AsQueryable();
            if (search != null)
            {
                queryble = queryble.Where(x => x.NameEn.Contains(search) || x.Address.Contains(search));
            }
            queryble = order switch
            {
                PatientOrderingEnum.Name => queryble.OrderBy(x => x.NameEn),
                PatientOrderingEnum.Address => queryble.OrderBy(x => x.Address),
                PatientOrderingEnum.Age => queryble.OrderBy(x => x.Age),
                PatientOrderingEnum.ContactNumber => queryble.OrderBy(x => x.ContactNumber),
                _ => throw new ArgumentOutOfRangeException(
                    nameof(order),
                    $"Invalid ordering option: {order}. Please provide a valid value from the {nameof(PatientOrderingEnum)}.")
            };
            return queryble;
        }
    }
}
