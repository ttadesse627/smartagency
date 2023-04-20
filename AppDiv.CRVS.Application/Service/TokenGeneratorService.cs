
using AppDiv.CRVS.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppDiv.CRVS.Application.Service
{
    public class TokenGeneratorService : ITokenGeneratorService
    {

        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _expiryMinutes;

        public TokenGeneratorService(string key, string issueer, string audience, string expiryMinutes)
        {
            _key = key;
            _issuer = issueer;
            _audience = audience;
            _expiryMinutes = expiryMinutes;
        }

        public string GenerateJWTToken((string userId, string userName, IList<string> roles) userDetails)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var (userId, userName, roles) = userDetails;

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, userId),
                new Claim(ClaimTypes.Name, userName),
                new Claim("UserId", userId)
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));


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

        //      private async Task<string> generateToken(ApplicationUser user)
        // {
        //     var userRoles = await _userManager.GetRolesAsync(user);

        //     var claims = new List<Claim>
        //     {

        //        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
        //        new Claim(ClaimTypes.Email, user.Email)
        //    };
        //     foreach (var userRole in userRoles)
        //     {
        //         claims.Add(new Claim(ClaimTypes.Role, userRole));

        //     }
        //     var token = new JwtSecurityToken(
        //         issuer: _configuration["Jwt:Issuer"],
        //         audience: _configuration["Jwt:Audience"],
        //         claims: claims,
        //         expires: DateTime.UtcNow.AddDays(1),
        //         // AddDays(1),
        //         notBefore: DateTime.UtcNow,
        //         signingCredentials: new SigningCredentials(
        //             key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
        //             algorithm: SecurityAlgorithms.HmacSha256)
        //         );
        //     var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        //     return tokenString;
        // }
    }
}
