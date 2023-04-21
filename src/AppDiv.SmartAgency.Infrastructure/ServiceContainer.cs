using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AppDiv.SmartAgency.Domain;
using AppDiv.SmartAgency.Utility.Config;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Infrastructure.Persistence;
using AppDiv.SmartAgency.Domain.Repositories;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Infrastructure.Services;
using AppDiv.SmartAgency.Utility.Services;
using AppDiv.SmartAgency.Infrastructure.Context;

namespace AppDiv.SmartAgency.Infrastructure
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            // services.AddDbContext<SmartAgencyDbContext>(
            //     options => options.UseSqlServer(
            //         configuration.GetConnectionString("SmartAgencyConnectionString"),
            //         o => o.MigrationsAssembly(typeof(ServiceContainer).Assembly.FullName)
            //     ).EnableSensitiveDataLogging()
            // );
            services.AddDbContext<SmartAgencyDbContext>(
                options =>
            options.UseMySql(configuration.GetConnectionString("SmartAgencyConnectionString"),
                Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"),
                mySqlOptions => mySqlOptions.EnableRetryOnFailure()));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<SmartAgencyDbContext>()
                    .AddDefaultTokenProviders();


            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false; // For special character
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.User.RequireUniqueEmail = true;
            });

            // services.Configure<RabbitMQConfiguration>(configuration.GetSection(RabbitMQConfiguration.CONFIGURATION_SECTION));
            // services.Configure<SMTPServerConfiguration>(configuration.GetSection(SMTPServerConfiguration.CONFIGURATION_SECTION));

            services.AddSingleton<IUserResolverService, UserResolverService>();
            services.AddSingleton<IMailService, MailKitService>();
            #region Repositories DI         

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            #endregion Repositories DI

            return services;
        }
    }
}
