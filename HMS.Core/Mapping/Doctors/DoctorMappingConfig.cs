using HMS.Core.Features.Doctors.Queries.Results;
using HMS.Data.Entities;
using Mapster;

namespace HMS.Core.Mapping.Doctors
{
    public class DoctorMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Doctor, GetDoctorDetailDto>()
                  .Map(dest => dest.Name,
                   src => src.Localize(src.NameAr, src.NameEn));
            // if inherit from LocalizableEntity
            //.Map(dest => dest.Name, src => src.GetLocalized());

            config.NewConfig<Doctor, GetDoctorListDto>()
                  .Map(dest => dest.Name,
                   src => src.Localize(src.NameAr, src.NameEn));



        }
    }
}
