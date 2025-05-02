using HMS.Data.Entities.Identity;
using HMS.Data.Results;

namespace HMS.Service.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResult> GetJWTToken(User user);

    }
}
