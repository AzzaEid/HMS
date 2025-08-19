using HMS.Data.Entities.Identity;

namespace HMS.Service.Abstracts
{
    public interface IApplicationUserService
    {
        public Task<string> AddUserAsync(User user, string password);
    }
}
