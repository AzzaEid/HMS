using HMS.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HMS.Infrustructure.Seeder
{
    public class RoleClaimsSeeder
    {
        public static async Task SeedAsync(RoleManager<Role> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("admin");

            if (adminRole != null)
            {
                var roleClaims = await roleManager.GetClaimsAsync(adminRole);

                if (!roleClaims.Any(c => c.Type == "Create Doctor"))
                {
                    await roleManager.AddClaimAsync(adminRole, new Claim("Create Doctor", "True"));
                }

                if (!roleClaims.Any(c => c.Type == "Delete Doctor"))
                {
                    await roleManager.AddClaimAsync(adminRole, new Claim("Delete Doctor", "True"));
                }

                if (!roleClaims.Any(c => c.Type == "Edit Doctor"))
                {
                    await roleManager.AddClaimAsync(adminRole, new Claim("Edit Doctor", "True"));
                }
            }
        }


    }
}
