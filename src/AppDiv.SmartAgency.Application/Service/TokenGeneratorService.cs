using AppDiv.SmartAgency.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppDiv.SmartAgency.Application.Service
{
    public class TokenGeneratorService(string key, string issueer, string audience, string expiryMinutes) : ITokenGeneratorService
    {
        private readonly string _key = key;
        private readonly string _issuer = issueer;
        private readonly string _audience = audience;
        private readonly string _expiryMinutes = expiryMinutes;
        public string GenerateJWTToken((string userId, string userName, IList<string> permissions) userDetails)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var (userId, userName, permissions) = userDetails;

            var claims = new List<Claim>()
            {
                new(JwtRegisteredClaimNames.Sub, userId),
                new(JwtRegisteredClaimNames.Jti, userName),
                new(ClaimTypes.Name, userName),
            };
            claims.AddRange(permissions.Select(role => new Claim(CustomClaims.Permissions, role)));

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_expiryMinutes)),
                signingCredentials: signingCredentials
           );

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedToken;
        }
    }
}
