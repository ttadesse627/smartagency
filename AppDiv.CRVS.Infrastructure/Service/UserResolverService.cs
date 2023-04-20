
using AppDiv.CRVS.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Http;

namespace AppDiv.CRVS.Infrastructure.Services
{
    public class UserResolverService: IUserResolverService
    {
        private readonly IHttpContextAccessor httpContext;

        public UserResolverService(IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;
        }

        public string GetUserEmail()
        {
            return httpContext.HttpContext.User?.Claims?.SingleOrDefault(p => p.Type == "Email")?.Value;
        }

        public Guid GetUserId()
        {
            return new Guid(httpContext.HttpContext.User?.Claims?.SingleOrDefault(p => p.Type == "UserId")?.Value);
        }

        public string GetLocale()
        {
            if (httpContext.HttpContext != null && httpContext.HttpContext.Request.Query.ContainsKey("locale"))
            {
                return httpContext.HttpContext.Request.Query["locale"].ToArray()[0];
            }
            return string.Empty;
        }
    }
}
