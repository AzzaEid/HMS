namespace HMS.Service.Abstracts
{
    public interface IAuthorizationService
    {
        public Task<bool> AddRoleAsync(string roleName);
        public Task<bool> IsRoleExistByName(string roleName);

    }
}
