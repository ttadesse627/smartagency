using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Infrastructure.Persistence;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Infrastructure.Services;
using AppDiv.SmartAgency.Utility.Services;
using AppDiv.SmartAgency.Infrastructure.Context;
using AppDiv.SmartAgency.Utility.Config;
using Microsoft.AspNetCore.Authorization;
using ClassScheduler.Infrastructure.Authentication;

namespace AppDiv.SmartAgency.Infrastructure
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<SmartAgencyDbContext>(
                options =>
            options.UseMySql(configuration.GetConnectionString("ConnectionString"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("ConnectionString")),
                mySqlOptions => mySqlOptions.EnableRetryOnFailure())
                .EnableSensitiveDataLogging(), ServiceLifetime.Scoped);


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
            // services.Configure<TwilioConfiguration>(configuration.GetSection(TwilioConfiguration.CONFIGURATION_SECTION));

            services.Configure<SMTPServerConfiguration>(configuration.GetSection(SMTPServerConfiguration.CONFIGURATION_SECTION));
            services.Configure<AfroMessageConfiguration>(configuration.GetSection(AfroMessageConfiguration.CONFIGURATION_SECTION));



            services.AddSingleton<IUserResolverService, UserResolverService>();
            services.AddSingleton<IMailService, MailKitService>();
            #region Repositories DI         
            services.AddTransient<ISmartAgencyDbContext, SmartAgencyDbContext>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IAttachmentRepository, AttaachmentRepository>();
            services.AddTransient<ILookUpRepository, LookUpRepository>();
            services.AddTransient<IPartnerRepository, PartnerRepository>();
            services.AddTransient<IApplicantRepository, ApplicantRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IOnlineApplicantRepository, OnlineApplicantRepository>();
            services.AddTransient<IDepositRepository, DepositRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IApplicantFollowupStatusRepository, ApplicantFollowupStatusRepository>();
            services.AddTransient<IPageRepository, PageRepository>();
            services.AddTransient<ICompanyInformationRepository, CompanyInformationRepository>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IProcessRepository, ProcessRepository>();
            services.AddTransient<IProcessDefinitionRepository, ProcessDefinitionRepository>();
            services.AddTransient<IApplicantProcessRepository, ApplicantProcessRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IEnjazRepository, EnjazRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IComplaintRepository, ComplaintRepository>();
            services.AddTransient<ITicketReadyRepository, TicketReadyRepository>();
            services.AddTransient<ITicketRefundRepository, TicketRefundRepository>();
            services.AddTransient<ITicketRebookRepository, TicketRebookRepository>();
            services.AddTransient<ITicketRegistrationRepository, TicketRegistrationRepository>();
            services.AddTransient<ITraveledApplicantRepository, TraveledApplicantRepository>();
            services.AddTransient<IRevocationTokenRepository, RevocationTokenRepository>();
            services.AddTransient<ILoginHistoryRepository, LoginHistoryRepository>();
            services.AddTransient<IRequestedApplicantRepository, RequestedApplicantRepository>();



            services.AddSingleton<IMailService, MailKitService>();
            services.AddSingleton<ISmsService, AfroMessageService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IReportsRepository, ReportsRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();

            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

            services.AddHttpContextAccessor();

            #endregion Repositories DI
            return services;
        }
    }
}
