using HMS.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace HMS.Infrustructure.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<Role> _roleManager)
        {
            var roles = new[] { "Admin", "Doctor", "Patient", "Pharmacist", "Accountant" };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new Role { Name = role });
                }
            }


        }
    }
}
