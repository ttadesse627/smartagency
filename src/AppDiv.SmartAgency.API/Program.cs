using AppDiv.SmartAgency.Application;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AppDiv.SmartAgency.Application.Service;
using AppDiv.SmartAgency.Application.Interfaces;
using AppDiv.SmartAgency.Infrastructure;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using AppDiv.SmartAgency.API.Middleware;
using AppDiv.SmartAgency.Utility.Services;



var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel(options =>
{
    options.Listen(IPAddress.Any, port: 8000);
});

builder.Services.AddControllers()
                .AddNewtonsoftJson();

Console.WriteLine(Environment.CurrentDirectory);

// For authentication
var _key = builder.Configuration["Jwt:Key"];
if (Encoding.UTF8.GetByteCount(_key) < 32)
{
    _key = RandomKeyGenerator.generateRandomKey();
}
// var _key = RandomKeyGenerator.generateRandomKey();
var _issuer = builder.Configuration["Jwt:Issuer"];
var _audience = builder.Configuration["Jwt:Audience"];
var _expirtyMinutes = builder.Configuration["Jwt:ExpiryMinutes"];


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
        ValidAudience = _audience,
        ValidIssuer = _issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),
        ClockSkew = TimeSpan.FromMinutes(Convert.ToDouble(_expirtyMinutes)),
        NameClaimType = ClaimTypes.NameIdentifier
    };
    x.Events = new JwtBearerEvents
    {
        OnTokenValidated = async context =>
        {
            // Check if the token is still valid
            var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<ITokenValidatorService>();
            var isValid = await tokenValidatorService.ValidateAsync(context.SecurityToken as JwtSecurityToken);
            if (!isValid)
            {
                context.Fail("Unauthorized Access");
            }
            return;
        },
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


// Dependency injection with key
builder.Services.AddSingleton<ITokenGeneratorService>(new TokenGeneratorService(_key, _issuer, _audience, _expirtyMinutes));

// Include Infrastructure/Application Dependency
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
                        Array.Empty<string>()

                    }
                });

});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Bearer", policy =>
    {
        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("UserId");
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionMiddleware();
app.UseHttpsRedirection();

// Must be betwwen app.UseRouting() and app.UseEndPoints()
// maintain middleware order
app.UseCors("CorsPolicy");

app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();
//app.UseMiddleware<AuthMiddleware>();

app.Run();