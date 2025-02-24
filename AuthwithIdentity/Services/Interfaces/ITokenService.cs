using AuthwithIdentity.DbContext;
using AuthwithIdentity.Models;
using System.IdentityModel.Tokens.Jwt;

namespace AuthwithIdentity.Services.Interfaces
{
    public interface ITokenService
    {
        public Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user);
        public Task<string> GenerateAndStoreRefreshToken(ApplicationUser user, ApplicationDbContext context);

       
    }
}
