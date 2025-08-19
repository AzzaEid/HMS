using HMS.Data.Entities;
using HMS.Infrustructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrustructure.Seeder
{
    public class DepartmentsSeeder
    {
        public static async Task SeedAsync(ApplicationDBContext context)
        {
            if (!await context.Departments.AnyAsync())
            {
                var departments = new[]
                {
                    new Department { DepartmentName = "Emergency Department" },
                    new Department { DepartmentName = "Intensive Care Unit" },
                    new Department { DepartmentName = "Operating Theater" },
                    new Department { DepartmentName = "Outpatient Department" },
                    new Department { DepartmentName = "Radiology Department" },
                    new Department { DepartmentName = "Laboratory" },
                    new Department { DepartmentName = "Pharmacy" }
                };

                await context.Departments.AddRangeAsync(departments);
                await context.SaveChangesAsync();

            }
        }
    }
}
