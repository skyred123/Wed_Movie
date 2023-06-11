using Microsoft.AspNetCore.Identity;

namespace Wed_Movie.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? TimeToken { get; set; }
    }
}
