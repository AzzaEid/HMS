using HMS.Data.Entities.Identity;
using HMS.Data.Helper;
using HMS.Data.Requests;
using HMS.Data.Results;
using HMS.Infrustructure.Data;
using HMS.Service.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HMS.Service.Implementations
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDBContext _dbContext;
        #endregion
        #region Constructors
        public AuthorizationService(RoleManager<Role> roleManager,
                                    UserManager<User> userManager,
                                    ApplicationDBContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
        }


        #endregion
        #region Roles 
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

        public async Task<string> EditRoleAsync(EditRoleRequest request)
        {
            //check role is exist or not
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role == null)
                return "notFound";
            role.Name = request.Name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded) return "Success";
            var errors = string.Join("-", result.Errors);
            return errors;
        }
        public async Task<string> DeleteRoleAsync(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return "NotFound";

            var users = await _userManager.GetUsersInRoleAsync(role.Name);

            if (users != null && users.Count() > 0) return "Used";

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded) return "Success";

            var errors = string.Join(",", result.Errors);
            return errors;
        }

        public async Task<bool> IsRoleExistById(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return false;
            else return true;
        }

        public async Task<List<Role>> GetRolesList()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await _roleManager.FindByIdAsync(id.ToString());
        }
        #endregion
        #region User roles
        public async Task<ManageUserRolesResult> ManageUserRolesData(User user)
        {
            var roles = await _roleManager.Roles.ToListAsync();

            var userRoles = new List<UserRoles>();
            foreach (var role in roles)
            {
                var hasRole = await _userManager.IsInRoleAsync(user, role.Name);
                userRoles.Add(new UserRoles
                {
                    Id = role.Id,
                    Name = role.Name,
                    HasRole = hasRole
                });
            }

            return new ManageUserRolesResult
            {
                UserId = user.Id,
                userRoles = userRoles
            };
        }
        public async Task<string> UpdateUserRoles(UpdateUserRolesRequest request)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user is null)
                    return "UserNotFound";

                var currentRoles = await _userManager.GetRolesAsync(user);

                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                    return "RemoveRolesFailed";

                var newRoles = request.userRoles
                    .Where(r => r.HasRole)
                    .Select(r => r.Name)
                    .ToList();

                var addResult = await _userManager.AddToRolesAsync(user, newRoles);
                if (!addResult.Succeeded)
                    return "AddRolesFailed";

                await transaction.CommitAsync();
                return "Success";
            }
            catch
            {
                await transaction.RollbackAsync();
                return "UpdateFailed";
            }
        }
        #endregion

        #region Manage Claims
        public async Task<ManageUserClaimsResult> ManageUserClaimData(User user)
        {
            var response = new ManageUserClaimsResult();
            var usercliamsList = new List<UserClaims>();
            response.UserId = user.Id;
            //Get USer Claims
            var userClaims = await _userManager.GetClaimsAsync(user);
            foreach (var claim in ClaimsStore.claims)
            {
                var userclaim = new UserClaims();
                userclaim.Type = claim.Type;
                if (userClaims.Any(x => x.Type == claim.Type))
                {
                    userclaim.Value = true;
                }
                else
                {
                    userclaim.Value = false;
                }
                usercliamsList.Add(userclaim);
            }
            response.userClaims = usercliamsList;
            //return Result
            return response;
        }

        #endregion
    }
}
