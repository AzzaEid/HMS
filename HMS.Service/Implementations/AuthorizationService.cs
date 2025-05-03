using HMS.Data.Entities.Identity;
using HMS.Infrustructure.Data;
using HMS.Service.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace HMS.Service.Implementations
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        #endregion
        #region Constructors
        public AuthorizationService(RoleManager<Role> roleManager,
                                    UserManager<User> userManager,
                                    ApplicationDBContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        #endregion
        #region handle Functions
        public async Task<bool> AddRoleAsync(string roleName)
        {
            var identityRole = new Role();
            identityRole.Name = roleName;
            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
                return true;
            return false;
        }

        public async Task<bool> IsRoleExistByName(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
        #endregion
    }
}
