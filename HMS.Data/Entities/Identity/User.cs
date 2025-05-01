using HMS.Data.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HMS.Data.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public int Age { get; set; }
        public Gender Gender { get; set; }
        [MaxLength(10)]
        public string ContactNumber { get; set; }
        public User()
        {
            //   UserRefreshTokens = new HashSet<UserRefreshToken>();
        }
    }
}
