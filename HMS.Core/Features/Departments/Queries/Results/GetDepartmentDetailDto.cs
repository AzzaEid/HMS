﻿using HMS.Core.Features.Doctors.Queries.Results;

namespace HMS.Core.Features.Departments.Queries.Results
{
    public class GetDepartmentDetailDto
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int ManagerDoctorId { get; set; }
        public string ManagerName { get; set; }
        public GetDoctorListDto ManagerDoctor { get; set; }
        public List<GetDoctorListDto> Doctors { get; set; }
    }
}
