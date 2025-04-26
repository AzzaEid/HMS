using HMS.Core.Features.Patients.Queries.Results;
using HMS.Data.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Core.Mapping.Patients
{
    public class PatientMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Patient, GetPatientListDto>()
                  .Map(dest => dest.PatientID, src => src.Id);
        }
    }
}
