
using MediatR;
using Microsoft.AspNetCore.Http;
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Utility.Exceptions;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;

namespace AppDiv.SmartAgency.Application.Features.Auth.Login
{
    public record LogoutCommand : IRequest<BaseResponse> { }
    public class LogoutCommandHandler(
        IRevocationTokenRepository tokenRepository, HttpClient httpClient,
        IUserResolverService userResolverService, IHttpContextAccessor httpContext,
        ILoginHistoryRepository loginHistoryRepository, IUserRepository userRepository) : IRequestHandler<LogoutCommand, BaseResponse>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ILoginHistoryRepository _loginHistoryRepository = loginHistoryRepository;
        private readonly IHttpContextAccessor _httpContext = httpContext;
        private readonly IUserResolverService _userResolverService = userResolverService;
        private readonly HttpClient _httpClient = httpClient;
        private readonly IRevocationTokenRepository _tokenRepository = tokenRepository;

        public async Task<BaseResponse> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            _httpContext.HttpContext!.Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
            var tokenValue = headerValue.FirstOrDefault();
            var res = new BaseResponse();

            if (tokenValue != null)
            {
                var rawToken = tokenValue["Bearer ".Length..];
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.ReadJwtToken(rawToken);

                // var tokenValidatorService = context.RequestServices.GetRequiredService<ITokenValidatorService>();
                // var isValid = await tokenValidatorService.ValidateAsync(token as JwtSecurityToken); 
                var userIdClaim = token.Claims.FirstOrDefault(c => c.Type == "UserId");

                // Get the user ID value from the claim, or return null if the claim is not found
                var UserId = (userIdClaim?.Value) ?? throw new NotFoundException("User Not Found");
                //  var response = await _userRepository.GetWithPredicateAsync(user => user.Id == UserId);
                // var responses = _userRepository.GetAll().Where(x => x.PersonalInfoId == UserId).FirstOrDefault();
                var tokenLogout = new RevocationToken
                {
                    Id = Guid.NewGuid(),
                    Token = tokenValue,
                    ExpirationDate = DateTime.Now.AddMonths(3)
                };

                var loginHistory = new LoginHistory
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    EventType = "Logout",
                    EventDate = DateTime.Now,
                    IpAddress = _httpContext.HttpContext.Connection.RemoteIpAddress!.ToString(),
                    Device = _httpContext.HttpContext.Request.Headers.UserAgent.ToString()
                };
                await _tokenRepository.InsertAsync(tokenLogout, cancellationToken);
                await _loginHistoryRepository.InsertAsync(loginHistory, cancellationToken);
                await _tokenRepository.SaveChangesAsync(cancellationToken);
                await _loginHistoryRepository.SaveChangesAsync(cancellationToken);

                res.Success = true;
                res.Message = "Loggedout successfully";


            }
            else
            {
                res.Success = false;
                res.Message = "invalid token";
            }
            return res;
        }
    }
}