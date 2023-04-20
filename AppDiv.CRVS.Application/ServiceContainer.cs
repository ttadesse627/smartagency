using Microsoft.Extensions.DependencyInjection;
using AppDiv.CRVS.Application.Interfaces;
using System.Reflection;
using MediatR;
using AppDiv.CRVS.Application.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace AppDiv.CRVS.Application
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, ConfigurationManager configuration) {

        
          
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());   

            
            services.AddScoped<IIdentityService, IdentityService>();

            return services;
        }
    }
}
