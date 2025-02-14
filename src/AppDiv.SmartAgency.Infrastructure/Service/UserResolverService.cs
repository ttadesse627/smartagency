﻿
using System.Security.Claims;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Http;

namespace AppDiv.SmartAgency.Infrastructure.Services
{
    public class UserResolverService : IUserResolverService
    {
        private readonly IHttpContextAccessor httpContext;

        public UserResolverService(IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;
        }

        public string? GetUserEmail()
        {
            return httpContext.HttpContext!.User?.Claims?.SingleOrDefault(p => p.Type == "Email")?.Value;
        }

        public Guid GetUserId()
        {
            var userIdClaim = httpContext.HttpContext!.User?.Claims?.SingleOrDefault(p => p.Type == "UserId");
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return userId;
            }
            return Guid.Empty;
            // return userId;
        }

        public string? GetLocale()
        {
            if (httpContext.HttpContext != null && httpContext.HttpContext.Request.Query.ContainsKey("locale"))
            {
                return httpContext.HttpContext.Request.Query["locale"].ToArray()[0];
            }

            return string.Empty;
        }

        public Guid GetId()
        {
            return new Guid();
        }
    }
}
