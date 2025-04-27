using HMS.Core.Features.Patients.Queries.Results;
using HMS.Data.Entities;
using Mapster;

namespace HMS.Core.Mapping.Patients
{
    public class PatientMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Patient, GetPatientListDto>()
                  .Map(dest => dest.PatientID, src => src.Id)
                  .Map(dest => dest.Name,
                   src => src.Localize(src.NameAr, src.NameEn));
            // if inherit from LocalizableEntity
            //.Map(dest => dest.Name, src => src.GetLocalized());

            config.NewConfig<Patient, GetPatientDetailDto>()
                  .Map(dest => dest.Name,
                   src => src.Localize(src.NameAr, src.NameEn));

            config.NewConfig<Patient, GetPatientPaginatedListResponse>()
                  .Map(dest => dest.PatientID, src => src.Id)
                  .Map(dest => dest.Name,
                   src => src.Localize(src.NameAr, src.NameEn));

        }
    }
}
