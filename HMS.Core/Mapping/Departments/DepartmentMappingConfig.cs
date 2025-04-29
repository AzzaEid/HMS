using HMS.Core.Features.Departments.Queries.Results;
using HMS.Data.Entities;
using Mapster;

namespace HMS.Core.Mapping.Departments
{
    public class DepartmentMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<Department, GetDepartmentListDto>.NewConfig()
                   .Map(dest => dest.ManagerName, src => src.ManagerDoctor.Localize(src.ManagerDoctor.NameAr, src.ManagerDoctor.NameEn));
            TypeAdapterConfig<Department, GetDepartmentDetailDto>.NewConfig()
                       .Map(dest => dest.ManagerName, src => src.ManagerDoctor.Localize(src.ManagerDoctor.NameAr, src.ManagerDoctor.NameEn))
                 .Map(dest => dest.Doctors, src => src.Doctors);

        }
    }
}
