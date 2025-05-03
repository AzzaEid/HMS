using HMS.Data.Entities.Enums;
using HMS.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace HMS.Infrustructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<User> _userManager)
        {
            string adminEmail = "admin@project.com";
            var adminUser = await _userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var defaultuser = new User()
                {
                    UserName = "admin",
                    Email = adminEmail,
                    ContactNumber = "123456",
                    Gender = Gender.Female,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                var result = await _userManager.CreateAsync(defaultuser, "ALeid#123");
                if (!result.Succeeded)
                {
                    throw new Exception("Field in seeding - create admin " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
                result = await _userManager.AddToRoleAsync(defaultuser, "Admin");
                if (!result.Succeeded)
                {
                    throw new Exception("Field in seeding - asign admin role " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
