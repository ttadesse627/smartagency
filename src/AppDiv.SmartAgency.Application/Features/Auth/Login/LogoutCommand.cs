// using AppDiv.CRVS.Application.Exceptions;
// using AppDiv.CRVS.Application.Contracts.DTOs;
using MediatR;
// using AppDiv.CRVS.Application.Interfaces;
// using Microsoft.Extensions.Logging;
// using AppDiv.CRVS.Domain.Repositories;
// using AppDiv.CRVS.Utility.Contracts;
// using AppDiv.CRVS.Domain;
// using AppDiv.CRVS.Application.Interfaces.Persistence;
// using AppDiv.CRVS.Domain.Entities;
using Microsoft.AspNetCore.Http;
// using AppDiv.CRVS.Application.Common;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.IdentityModel.Tokens;
// using System.Net.Http;
// using System;
// using System.IdentityModel.Tokens.Jwt;
// using System.Linq;
// using System.Text;
// // using Microsoft.Extensions.Primitives;
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Utility.Exceptions;
using Microsoft.Extensions.Primitives;

namespace AppDiv.SmartAgency.Application.Features.Auth.Login
{
    public class LogoutCommand : IRequest<BaseResponse> { }
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
            Guid UserId = _userResolverService.GetUserId();
            var res = new BaseResponse();
            if (UserId == null && UserId == Guid.Empty)
            {
                throw new NotFoundException("User Not Found");
            }
            var response = await _userRepository.GetWithPredicateAsync(user => user.Id == UserId.ToString());
            var tokenLogout = new RevocationToken
            {
                Id = Guid.NewGuid(),
                Token = headerValue.FirstOrDefault(),
                ExpirationDate = DateTime.Now.AddMonths(3)
            };
            var LoginHis = new LoginHistory
            {
                Id = Guid.NewGuid(),
                UserId = response.Id,
                EventType = "Logout",
                EventDate = DateTime.Now,
                IpAddress = _httpContext.HttpContext.Connection.RemoteIpAddress.ToString(),
                Device = _httpContext.HttpContext.Request.Headers["User-Agent"].ToString()
            };
            await _tokenRepository.InsertAsync(tokenLogout, cancellationToken);
            await _loginHistoryRepository.InsertAsync(LoginHis, cancellationToken);
            await _tokenRepository.SaveChangesAsync(cancellationToken);
            await _loginHistoryRepository.SaveChangesAsync(cancellationToken);
            res = new BaseResponse
            {
                Success = false,
                Message = "Loggedout successfully"
            };
            return res;
        }
    }
}