using Microsoft.IdentityModel.Tokens;
using Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Biblioteca.WebApi.Helpers
{
    public class TokenService
    {
        private readonly IConfiguration _config;
        public TokenService( IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(ApplicationUser user)
        {
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var issuer = _config["Jwt:Issuer"];

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer,
                issuer,
                claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
