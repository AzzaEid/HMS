namespace HMS.Core.Features.Departments.Queries.Results
{
    public class GetDepartmentListDto
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int ManagerDoctorId { get; set; }
        public string ManagerName { get; set; }

    }
}
