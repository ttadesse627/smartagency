
using System.IdentityModel.Tokens.Jwt;
using AppDiv.SmartAgency.Application.Interfaces;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
namespace AppDiv.SmartAgency.Application.Service
{
    public class TokenValidatorService : ITokenValidatorService
    {
        private readonly IRevocationTokenRepository _tokenRepository;
        public TokenValidatorService(IRevocationTokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }
        public async Task<bool> ValidateAsync(JwtSecurityToken token)
        {
            var httpContext = new HttpContextAccessor().HttpContext;
            httpContext.Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
            var tokenId = headerValue.FirstOrDefault();
            var expiredToken = await _tokenRepository.GetAllWithPredicateAsync(x => x.ExpirationDate <= DateTime.Now);
            if (expiredToken.FirstOrDefault() != null)
            {
                _tokenRepository.Delete(expiredToken.ToArray());
                _tokenRepository.SaveChanges();
            }
            if (string.IsNullOrEmpty(tokenId))
            {
                return true;
            }
            var tokenExists = await _tokenRepository.GetAllWithPredicateAsync(x => x.Token == tokenId);
            if (tokenExists.FirstOrDefault() == null)
            {
                return true;
            }
            return false;
        }
    }
}