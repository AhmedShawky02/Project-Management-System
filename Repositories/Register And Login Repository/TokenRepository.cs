using Azure.Core;
using Microsoft.IdentityModel.Tokens;
using Project_Management_System.Interfaces;
using Project_Management_System.Jwt_Options;
using Project_Management_System.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project_Management_System.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly JwtOptions _options;

        public TokenRepository(JwtOptions options)
        {
            _options = options;
        }
        public async Task<string> CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Email, user.Email)
            };

            if (user.UserRoles != null && user.UserRoles.Any())
            {
                foreach (var role in user.UserRoles)
                {
                    if (role.Role != null)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Role.RoleName));
                    }
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _options.Issuer,
                Audience = _options.Audience,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SigningKey)),
                    SecurityAlgorithms.HmacSha256),

                Subject = new ClaimsIdentity(claims)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return accessToken;
        }
    }
}