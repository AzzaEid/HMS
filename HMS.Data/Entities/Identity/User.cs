using HMS.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace HMS.Data.Entities.Identity
{
    //public class User : IdentityUser<int>
    public class User
    {
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }

        public int Age { get; set; }
        public Gender Gender { get; set; }
        [MaxLength(10)]
        public string ContactNumber { get; set; }
    }
}
