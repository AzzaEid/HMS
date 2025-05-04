using HMS.Data.Entities.Identity;
using HMS.Data.Requests;
using HMS.Data.Results;

namespace HMS.Service.Abstracts
{
    public interface IAuthorizationService
    {
        public Task<bool> AddRoleAsync(string roleName);
        public Task<bool> IsRoleExistByName(string roleName);
        public Task<string> EditRoleAsync(EditRoleRequest request);
        public Task<string> DeleteRoleAsync(int roleId);
        public Task<bool> IsRoleExistById(int roleId);
        public Task<List<Role>> GetRolesList();
        public Task<Role> GetRoleById(int id);
        public Task<ManageUserRolesResult> ManageUserRolesData(User user);

    }
}
