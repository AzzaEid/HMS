using HMS.Core.Features.Appointments.Queries.Results;
using HMS.Data.Entities;
using Mapster;

namespace HMS.Core.Mapping.Appointments
{
    public class AppointmentMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Appointment, GetAppointmentListDto>()
                  .Map(dest => dest.PatientName, src => src.Patient.Localize(src.Patient.NameAr, src.Patient.NameEn))
                  .Map(dest => dest.DoctorName, src => src.Doctor.Localize(src.Doctor.NameAr, src.Doctor.NameEn))
                  .Map(dest => dest.Status, src => src.Status.ToString());

            config.NewConfig<Appointment, GetAppointmentDetailDto>()
                  .Map(dest => dest.PatientName, src => src.Patient.Localize(src.Patient.NameAr, src.Patient.NameEn))
                  .Map(dest => dest.DoctorName, src => src.Doctor.Localize(src.Doctor.NameAr, src.Doctor.NameEn))
                  .Map(dest => dest.DoctorSpecialty, src => src.Doctor.Specialty.SpecialtyName)
                  .Map(dest => dest.Status, src => src.Status.ToString());

            config.NewConfig<Appointment, GetAppointmentPaginatedListResponse>()
                  .MapWith(src => new GetAppointmentPaginatedListResponse(
                      src.Id,
                      src.Patient.Localize(src.Patient.NameAr, src.Patient.NameEn),
                      src.Doctor.Localize(src.Doctor.NameAr, src.Doctor.NameEn),
                      src.Date,
                      src.Status.ToString()
                  ));
        }
    }
}
