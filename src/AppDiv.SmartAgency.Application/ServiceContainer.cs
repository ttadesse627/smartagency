using Microsoft.Extensions.DependencyInjection;
using AppDiv.SmartAgency.Application.Interfaces;
using System.Reflection;
using MediatR;
using AppDiv.SmartAgency.Application.Service;
using Microsoft.Extensions.Configuration;

namespace AppDiv.SmartAgency.Application
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(ServiceContainer).Assembly);

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ITokenValidatorService, TokenValidatorService>();
            services.AddScoped<HelperService>();

            services.AddHttpClient();

            return services;
        }
    }
}
