using System.Security.Claims;

namespace HMS.Data.Helper
{
    public static class ClaimsStore
    {
        public static List<Claim> claims = new()
        {
            new Claim("Create Doctor","false"),
            new Claim("Edit Doctor","false"),
            new Claim("Delete Doctor","false"),
        };
    }
}
