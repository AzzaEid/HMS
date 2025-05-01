using HMS.Data.Entities.Enums;

namespace HMS.Core.Features.ApplicationUser.Queries.Results
{
    public class GetUserPaginationReponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public Gender Gender { get; set; }
        public string? ContactNumber { get; set; }
    }
}
