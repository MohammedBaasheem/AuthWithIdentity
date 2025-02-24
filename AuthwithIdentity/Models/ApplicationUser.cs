using Microsoft.AspNetCore.Identity;

namespace AuthwithIdentity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
