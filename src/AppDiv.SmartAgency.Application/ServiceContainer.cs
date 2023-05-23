using Microsoft.Extensions.DependencyInjection;
using AppDiv.SmartAgency.Application.Interfaces;
using System.Reflection;
using MediatR;
using AppDiv.SmartAgency.Application.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Features.Applicants.Command.Update;

namespace AppDiv.SmartAgency.Application
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(ServiceContainer).Assembly);

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IRequestHandler<EditApplicantCommand, ServiceResponse<int>>, EditApplicantCommandHandler>();

            return services;
        }
    }
}
