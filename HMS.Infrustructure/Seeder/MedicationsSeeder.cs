using HMS.Data.Entities;
using HMS.Infrustructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrustructure.Seeder
{
    public class MedicationsSeeder
    {
        public static async Task SeedAsync(ApplicationDBContext context)
        {
            if (!await context.Medications.AnyAsync())
            {
                var medications = new[]
                {
                    new Medication { Name = "Paracetamol 500mg", Quantity = 100, Price = 5.50m },
                    new Medication { Name = "Ibuprofen 400mg", Quantity = 80, Price = 8.75m },
                    new Medication { Name = "Amoxicillin 500mg", Quantity = 60, Price = 12.00m },
                    new Medication { Name = "Omeprazole 20mg", Quantity = 50, Price = 15.25m },
                    new Medication { Name = "Metformin 500mg", Quantity = 90, Price = 10.00m },
                    new Medication { Name = "Lisinopril 10mg", Quantity = 70, Price = 18.50m },
                    new Medication { Name = "Atorvastatin 20mg", Quantity = 40, Price = 22.75m },
                    new Medication { Name = "Aspirin 100mg", Quantity = 120, Price = 3.25m }
                };

                await context.Medications.AddRangeAsync(medications);
                await context.SaveChangesAsync();

            }
        }
    }
}
