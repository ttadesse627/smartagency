using System.IdentityModel.Tokens.Jwt;

namespace AppDiv.SmartAgency.Application.Interfaces
{
    public interface ITokenValidatorService
    {
        public Task<bool> ValidateAsync(JwtSecurityToken token);
    }
}
