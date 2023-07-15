## This the first one

using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace WebApi.Services;
public class CustomAuthorizeAttribute : TypeFilterAttribute
{
public CustomAuthorizeAttribute(string ControllerName, string ControllerAction)
: base(typeof(AuthorizeActionFilter))
{
Arguments = new object[] { ControllerName, ControllerAction };
}
}

public class AuthorizeActionFilter : IAuthorizationFilter
{
private readonly string \_ControllerName;
private readonly string \_ControllerAction;
private readonly IIdentityService \_identityService;
private readonly IAppDbContext \_dbContext;

    public AuthorizeActionFilter(string ControllerName, string ControllerAction, IIdentityService identityService, IAppDbContext dbContext)
    {
        _ControllerName = ControllerName;
        _ControllerAction = ControllerAction;
        _identityService = identityService;
        _dbContext = dbContext;
    }



    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // bool isAuthorized = MumboJumboFunction(context.HttpContext.User, _ControllerName, _ControllerAction); // :)

        var userId = context.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            context.Result = new ForbidResult();
            return;


        }
        var groupId = _identityService.GetUserGroupId(userId);
        var roles = _dbContext.AppUserRoles.Where(r => r.UserGroupId == groupId);
        var role = roles.Where(r => r.Page.ToLower() == _ControllerName.ToLower()).FirstOrDefault();
        bool isAuthorized = false;

        if (role == null)
        {
            context.Result = new ForbidResult();
            return;
        }

        switch (_ControllerAction)
        {
            case "ReadAll":
                isAuthorized = role.CanView; break;
            case "ReadSingle":
                isAuthorized = role.CanViewDetail; break;
            case "Update":
                isAuthorized = role.CanUpdate; break;
            case "Delete":
                isAuthorized = role.CanDelete; break;
            case "Add":
                isAuthorized = role.CanAdd; break;
        }
        if (!isAuthorized)
        {
            context.Result = new ForbidResult();
        }


    }

}

## This is Another

using Application.Common.Exceptions;
using Application.CompanyModule.Commands.CreateCompanyCommand;
using Application.CompanyModule.Commands.DeleteCompanyCommand;
using Application.CompanyModule.Commands.UpdateCompanyCommand;
using Application.CompanyModule.Queries.GetAllCompanyQuery;
using Application.CompanyModule.Queries.GetCompanyBankInformation;
using Application.CompanyModule.Queries.GetCompanyQuery;
using Application.ContactPersonModule.Queries;
using Application.DriverModule.Queries.GetCompanyLookup;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
public class CompanyController : ApiControllerBase
{
[HttpPost]
[Route("create")]
[CustomAuthorizeAttribute("Company", "Add")]
public async Task<ActionResult> create([FromBody] CreateCompanyCommand command)

        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

        [HttpPut]
        [CustomAuthorizeAttribute("Company", "Update")]
        public async Task<ActionResult> update([FromBody] UpdateCompanyCommand command)
        {

            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

        [HttpGet("{id}")]
        [CustomAuthorizeAttribute("Company", "ReadSingle")]
        public async Task<ActionResult> view(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetCompanyQuery(id)));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }
        [HttpGet]
        [Route("lookup")]
        public async Task<ActionResult> lookup()
        {
            try
            {
                return Ok(await Mediator.Send(new GetCompanyLookupQuery()));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }
        [HttpGet]
        [CustomAuthorizeAttribute("Company", "ReadAll")]
        // [Authorize]
        public async Task<ActionResult> list([FromQuery] GetAllCompanies command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }
        [HttpGet]
        [Route("nameOnPermit/{companyId}")]
        [CustomAuthorizeAttribute("Company", "ReadSingle")]
        public async Task<ActionResult> nameOnPermits(int companyId)
        {
            try
            {
                return Ok(await Mediator.Send(new GetContactPeopleByCompanyIdQuery { CompanyId = companyId }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }
        [HttpGet]
        [Route("bankInformation/{companyId}")]
        [CustomAuthorizeAttribute("Company", "ReadSingle")]
        public async Task<ActionResult> bankInformation(int companyId)
        {
            try
            {
                return Ok(await Mediator.Send(new GetCompanyBankInformationQuery { CompanyId = companyId }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }
        }
        [HttpDelete("{id}")]
        [CustomAuthorizeAttribute("Company", "Delete")]
        public async Task<ActionResult> delete(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new DeleteCompany { Id = id }));
            }
            catch (GhionException ex)
            {
                return AppdiveResponse.Response(this, ex.Response);
            }

        }

    }

}

## Authentication and Autherization

### 1.

using AppDiv.CRVS.Application.Exceptions;
using AppDiv.CRVS.Application.Contracts.DTOs;
using MediatR;
using AppDiv.CRVS.Application.Interfaces;
using Microsoft.Extensions.Logging;
using AppDiv.CRVS.Domain.Repositories;
using AppDiv.CRVS.Utility.Contracts;
using AppDiv.CRVS.Domain;
using AppDiv.CRVS.Application.Interfaces.Persistence;
using AppDiv.CRVS.Domain.Entities;
using Microsoft.AspNetCore.Http;
using AppDiv.CRVS.Application.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Primitives;
namespace AppDiv.CRVS.Application.Features.Auth.Login
{
public class LogoutCommand : IRequest<BaseResponse>
{
}
public class LogoutCommandHandler : IRequestHandler<LogoutCommand, BaseResponse>
{
private readonly IUserRepository \_userRepository;
private readonly ILoginHistoryRepository \_loginHistoryRepository;
private readonly IHttpContextAccessor \_httpContext;
private readonly IUserResolverService \_userResolverService;
private readonly HttpClient \_httpClient;
private readonly IRevocationTokenRepository \_tokenRepository;
public LogoutCommandHandler(IRevocationTokenRepository tokenRepository, HttpClient httpClient, IUserResolverService userResolverService, IHttpContextAccessor httpContext, ILoginHistoryRepository loginHistoryRepository, IUserRepository userRepository)
{
\_userRepository = userRepository;
\_loginHistoryRepository = loginHistoryRepository;
\_httpContext = httpContext;
\_userResolverService = userResolverService;
\_httpClient = httpClient;
\_tokenRepository = tokenRepository;
}
public async Task<BaseResponse> Handle(LogoutCommand request, CancellationToken cancellationToken)
{
\_httpContext.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
Guid UserId = \_userResolverService.GetUserPersonalId();
var res = new BaseResponse();
if (UserId == null && UserId == Guid.Empty)
{
throw new NotFoundException("User Not Found");
}
var response = \_userRepository.GetAll().Where(x => x.PersonalInfoId == UserId).FirstOrDefault();
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
IpAddress = \_httpContext.HttpContext.Connection.RemoteIpAddress.ToString(),
Device = \_httpContext.HttpContext.Request.Headers["User-Agent"].ToString()
};
await \_tokenRepository.InsertAsync(tokenLogout, cancellationToken);
await \_loginHistoryRepository.InsertAsync(LoginHis, cancellationToken);
await \_loginHistoryRepository.SaveChangesAsync(cancellationToken);
res = new BaseResponse
{
Success = false,
Message = "Logout successfully"
};
return res;
}
}
}

### 2.

<
<using AppDiv.CRVS.API.Helpers;
using AppDiv.CRVS.Application;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AppDiv.CRVS.Application.Service;
using AppDiv.CRVS.Application.Interfaces;
using AppDiv.CRVS.Infrastructure;
using AppDiv.CRVS.Api.Middleware;
using System.Security.Claims;
using AppDiv.CRVS.Infrastructure.Hub;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Cors;
using AppDiv.CRVS.Infrastructure.Hub.ChatHub;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddNewtonsoftJson(); ;
// For authentication
var \_key = builder.Configuration["Jwt:Key"];
var \_issuer = builder.Configuration["Jwt:Issuer"];
var \_audience = builder.Configuration["Jwt:Audience"];
var \_expirtyMinutes = builder.Configuration["Jwt:ExpiryMinutes"];
// Configuration for token
builder.Services.AddAuthentication(x =>
{
x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
x.RequireHttpsMetadata = false;
x.SaveToken = true;
x.TokenValidationParameters = new TokenValidationParameters()
{
ValidateIssuer = true,
ValidateAudience = true,
ValidateLifetime = true,
ValidateIssuerSigningKey = true,
ValidAudience = \_audience,
ValidIssuer = \_issuer,
IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(\_key)),
ClockSkew = TimeSpan.FromMinutes(Convert.ToDouble(\_expirtyMinutes)),
NameClaimType = ClaimTypes.NameIdentifier
};
x.Events = new JwtBearerEvents
{
// OnTokenValidated = async context =>
// {
// // Check if the token is still valid
// var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<ITokenValidatorService>();
// var isValid = await tokenValidatorService.ValidateAsync(context.SecurityToken as JwtSecurityToken);
// if (!isValid)
// {
// context.Fail("Unauthorized Access");
// }
// return;
// },
OnMessageReceived = context =>
{
var accessToken = context.Request.Query["access_token"];
if (!string.IsNullOrEmpty(accessToken))
{
context.Token = accessToken;
}
return Task.CompletedTask;
}
};
});
builder.Services.AddSingleton<ITokenGeneratorService>(new TokenGeneratorService(\_key, \_issuer, \_audience, \_expirtyMinutes));
builder.Services.AddApplication(builder.Configuration)
.AddInfrastructure(builder.Configuration);
builder.Services.AddSignalR();
builder.Services.AddCors(c =>
{
c.AddPolicy("CorsPolicy",
options =>
options.WithOrigins("http://localhost:4200")
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
// To enable authorization using swagger (Jwt)
c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
{
Name = "Authorization",
Type = SecuritySchemeType.ApiKey,
Scheme = "Bearer",
BearerFormat = "JWT",
In = ParameterLocation.Header,
Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer {token}\"",
});
c.AddSecurityRequirement(new OpenApiSecurityRequirement
{
{
new OpenApiSecurityScheme
{
Reference = new OpenApiReference
{
Type = ReferenceType.SecurityScheme,
Id = "Bearer"
}
},
new string[] {}
}
});
});
builder.Services.AddAuthorization(options =>
{
options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy =>
{
policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
policy.RequireClaim(ClaimTypes.NameIdentifier);
});
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
// app.MigrateDatabase();
using (var scope = app.Services.CreateScope())
{
var initialiser = scope.ServiceProvider.GetRequiredService<CRVSDbContextInitializer>();
await initialiser.InitialiseAsync();
await initialiser.SeedAsync();
}
app.UseSwagger();
app.UseSwaggerUI();
}
app.ConfigureExceptionMiddleware();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseCors("CorsPolicy");
app.UseEndpoints(endpoints =>
{
endpoints.MapControllers();
endpoints.MapHub<MessageHub>("/Notification");
endpoints.MapHub<ChatHub>("/Chat");
});
app.MapControllers();
app.Run();
