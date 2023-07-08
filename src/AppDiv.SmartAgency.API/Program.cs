using AppDiv.SmartAgency.Application;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AppDiv.SmartAgency.Application.Service;
using AppDiv.SmartAgency.Application.Interfaces;
using AppDiv.SmartAgency.Infrastructure;
using AppDiv.SmartAgency.Api.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .AddNewtonsoftJson();

// For authentication
var _key = builder.Configuration["Jwt:Key"];
var _issuer = builder.Configuration["Jwt:Issuer"];
var _audience = builder.Configuration["Jwt:Audience"];
var _expirtyMinutes = builder.Configuration["Jwt:ExpiryMinutes"];


// Configuration for token
// builder.Services.AddAuthentication(x =>
// {
//     x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//     x.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
// }).AddJwtBearer(x =>
// {
//     x.RequireHttpsMetadata = false;
//     x.SaveToken = true;
//     x.TokenValidationParameters = new TokenValidationParameters()
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidateLifetime = true,
//         ValidateIssuerSigningKey = true,
//         ValidAudience = _audience,
//         ValidIssuer = _issuer,
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),
//         ClockSkew = TimeSpan.FromMinutes(Convert.ToDouble(_expirtyMinutes))

//     };
// }).AddCookie(options =>
// {
//     options.LoginPath = "/api/auth/login";
//     options.LogoutPath = "/api/auth/logout";
//     options.AccessDeniedPath = "/api/auth/access-denied";
// });


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/api/auth/login";
    options.LogoutPath = "/api/auth/logout";
    options.AccessDeniedPath = "/api/auth/access-denied";
    options.SlidingExpiration = false;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = _issuer,
        ValidAudience = _audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),
        ClockSkew = TimeSpan.FromMinutes(Convert.ToDouble(_expirtyMinutes)),
        ValidateLifetime = true,
        LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken token, TokenValidationParameters parameters) =>
        {
            if (expires != null && expires <= DateTime.UtcNow)
            {
                return false;
            }

            return true;
        }
    };
});


// Dependency injection with key
builder.Services.AddSingleton<ITokenGeneratorService>(new TokenGeneratorService(_key, _issuer, _audience, _expirtyMinutes));

// Include Infrastructur/Application Dependency
builder.Services.AddApplication(builder.Configuration)
                .AddInfrastructure(builder.Configuration);

builder.Services.AddCors(c =>
{
    c.AddPolicy("CorsPolicy", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
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
    options.AddPolicy("Bearer", policy =>
    {
        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("UserId");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MigrateDatabase();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionMiddleware();
app.UseHttpsRedirection();

// Must be betwwen app.UseRouting() and app.UseEndPoints()
// maintain middleware order
app.UseCors("CorsPolicy");

// Added for authentication
// Maintain middleware order
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();