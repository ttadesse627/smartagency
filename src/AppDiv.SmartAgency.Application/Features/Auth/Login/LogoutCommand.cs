
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
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, BaseResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoginHistoryRepository _loginHistoryRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUserResolverService _userResolverService;
        private readonly HttpClient _httpClient;
        private readonly IRevocationTokenRepository _tokenRepository;
        public LogoutCommandHandler(IRevocationTokenRepository tokenRepository, HttpClient httpClient, IUserResolverService userResolverService, IHttpContextAccessor httpContext, ILoginHistoryRepository loginHistoryRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _loginHistoryRepository = loginHistoryRepository;
            _httpContext = httpContext;
            _userResolverService = userResolverService;
            _httpClient = httpClient;
            _tokenRepository = tokenRepository;
        }
        public async Task<BaseResponse> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            _httpContext.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues headerValue);


            var tokenValue = headerValue.FirstOrDefault();
            var res = new BaseResponse();

            if (tokenValue != null)
            {
                var rawToken = tokenValue.Substring("Bearer ".Length);
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.ReadJwtToken(rawToken);

                // var tokenValidatorService = context.RequestServices.GetRequiredService<ITokenValidatorService>();
                // var isValid = await tokenValidatorService.ValidateAsync(token as JwtSecurityToken); 
                var userIdClaim = token.Claims.FirstOrDefault(c => c.Type == "UserId");

                // Get the user ID value from the claim, or return null if the claim is not found
                var UserId = userIdClaim?.Value;


                //Guid UserId = _userResolverService.GetUserId();

                if (UserId == null)
                {
                    throw new NotFoundException("User Not Found");
                }
                //  var response = await _userRepository.GetWithPredicateAsync(user => user.Id == UserId);
                // var responses = _userRepository.GetAll().Where(x => x.PersonalInfoId == UserId).FirstOrDefault();
                var tokenLogout = new RevocationToken
                {
                    Id = Guid.NewGuid(),
                    Token = tokenValue,
                    ExpirationDate = DateTime.Now.AddMonths(3)
                };


                var LoginHis = new LoginHistory
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    EventType = "Logout",
                    EventDate = DateTime.Now,
                    IpAddress = _httpContext.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Device = _httpContext.HttpContext.Request.Headers["User-Agent"].ToString()
                };
                await _tokenRepository.InsertAsync(tokenLogout, cancellationToken);
                await _loginHistoryRepository.InsertAsync(LoginHis, cancellationToken);
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