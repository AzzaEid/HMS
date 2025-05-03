using HMS.Data.Entities.Identity;
using HMS.Infrustructure.Bases;

namespace HMS.Infrustructure.Abstract
{
    public interface IRefreshTokenRepository : IGenericRepository<UserRefreshToken>
    {
    }
}
