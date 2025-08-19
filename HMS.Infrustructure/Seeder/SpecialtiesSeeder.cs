using HMS.Data.Entities;
using HMS.Infrustructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrustructure.Seeder
{
    public class SpecialtiesSeeder
    {
        public static async Task SeedAsync(ApplicationDBContext context)
        {
            if (!await context.Specialties.AnyAsync())
            {
                var specialties = new[]
                {
                    new Specialty { SpecialtyName = "Cardiology" },
                    new Specialty { SpecialtyName = "Neurology" },
                    new Specialty { SpecialtyName = "Orthopedics" },
                    new Specialty { SpecialtyName = "Pediatrics" },
                    new Specialty { SpecialtyName = "Dermatology" },
                    new Specialty { SpecialtyName = "Internal Medicine" },
                    new Specialty { SpecialtyName = "Surgery" },
                    new Specialty { SpecialtyName = "Psychiatry" }
                };

                await context.Specialties.AddRangeAsync(specialties);
                await context.SaveChangesAsync();

            }
        }
    }
}
