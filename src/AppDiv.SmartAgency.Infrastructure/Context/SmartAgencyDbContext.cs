using AppDiv.SmartAgency.Domain.Configuration.Settings;
using AppDiv.SmartAgency.Domain.Entities.Audit;
using AppDiv.SmartAgency.Domain.Entities.Settings;
using AppDiv.SmartAgency.Domain.Entities.Base;
using Audit.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Audit.EntityFramework;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Configurations;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Infrastructure.Seed;
using AppDiv.SmartAgency.Domain.Entities.TicketData;

namespace AppDiv.SmartAgency.Infrastructure.Context
{
    public class SmartAgencyDbContext : AuditIdentityDbContext<ApplicationUser>, ISmartAgencyDbContext
    {
        private readonly IUserResolverService _userResolverService;

        public DbSet<AuditLog> AuditLogs { get; set; } = null!;
        public DbSet<Suffix> Suffixes { get; set; } = null!;
        public DbSet<Attachment> Attachments { get; set; } = null!;
        public DbSet<LookUp> LookUps { get; set; } = null!;
        public DbSet<Partner> Partners { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<Applicant> Applicants { get; set; } = null!;
        public DbSet<BankAccount> BankAccounts { get; set; } = null!;
        public DbSet<Beneficiary> Beneficiaries { get; set; } = null!;
        public DbSet<Education> Educations { get; set; } = null!;
        public DbSet<EmergencyContact> EmergencyContacts { get; set; } = null!;
        public DbSet<Experience> Experiences { get; set; } = null!;
        public DbSet<LanguageSkill> LanguageSkills { get; set; } = null!;
        public DbSet<Representative> Representatives { get; set; } = null!;
        public DbSet<Witness> Witnesses { get; set; } = null!;
        public DbSet<Skill> Skills { get; set; } = null!;
        public DbSet<QualificationType> QualificationTypes { get; set; } = null!;
        public DbSet<LevelOfQualification> LevelOfQualifications { get; set; } = null!;
        public DbSet<Award> Awards { get; set; } = null!;
        public DbSet<OnlineApplicant> OnlineApplicants { get; set; } = null!;
        public DbSet<Deposit> Deposits { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Sponsor> Sponsors { get; set; } = null!;
        public DbSet<OrderCriteria> OrderCriterias { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<Page> Pages { get; set; } = null!;
        public DbSet<ApplicantFollowupStatus> ApplicantFollowupStatuses { get; set; } = null!;
        public DbSet<CompanyInformation> CompanyInformations { get; set; } = null!;
        public DbSet<CompanySetting> CompanySettings { get; set; } = null!;
        public DbSet<CountryOperation> CountryOperations { get; set; } = null!;
        public DbSet<Process> Processes { get; set; } = null!;
        public DbSet<ProcessDefinition> ProcessDefinitions { get; set; } = null!;
        public DbSet<ApplicationUser> AppUsers { get; set; } = null!;
        public DbSet<UserGroup> UserGroups { get; set; } = null!;
        public DbSet<Permission> Permissions { get; set; } = null!;
        public DbSet<Enjaz> Enjazs { get; set; } = null!;
        public DbSet<ApplicantProcess> ApplicantProcesses { get; set; } = null!;
        public DbSet<Complaint> Complaints { get; set; } = null!;
        public DbSet<TicketReady> TicketReadies { get; set; } = null!;
        public DbSet<TicketRegistration> TicketRegistrations { get; set; } = null!;
        public DbSet<TicketRefund> TicketRefunds { get; set; } = null!;
        public DbSet<TicketRebook> TicketRebooks { get; set; } = null!;
        public DbSet<TicketRebookReg> TicketRebookRegistrations { get; set; } = null!;
        public DbSet<TraveledApplicant> TraveledApplicants { get; set; } = null!;
        public DbSet<RequestedApplicant> RequestedApplicants { get; set; } = null!;
        public DbSet<LoginHistory> LoginHistories { get; set; } = null!;
        public DbSet<RevocationToken> RevocationTokens { get; set; } = null!;
        public DbSet<Setting> Settings { get; set; } = null!;

        public SmartAgencyDbContext(DbContextOptions<SmartAgencyDbContext> options, IUserResolverService userResolverService) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            _userResolverService = userResolverService;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            // optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            // To run sql scripts, example alter database to set collation, create stored procedure, function, view ....
            // optionsBuilder.ReplaceService<IMigrationsSqlGenerator, CustomSqlServerMigrationsSqlGenerator>();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Entity Configuration
            {
                modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
                modelBuilder.ApplyConfiguration(new UserGroupEntityConfig());
                modelBuilder.ApplyConfiguration(new AddressEntityConfig());
                modelBuilder.ApplyConfiguration(new ApplicantEntityConfig());
                modelBuilder.ApplyConfiguration(new BeneficiaryEntityConfig());
                modelBuilder.ApplyConfiguration(new CustomerEntityConfiguration());
                modelBuilder.ApplyConfiguration(new EducationEntityConfig());
                modelBuilder.ApplyConfiguration(new EmergencyContactEntityConfig());
                modelBuilder.ApplyConfiguration(new ExperienceEntityConfig());
                modelBuilder.ApplyConfiguration(new LanguageSkillEntityConfig());
                modelBuilder.ApplyConfiguration(new CustomerEntityConfiguration());
                modelBuilder.ApplyConfiguration(new OnlineApplicantEntityConfiguration());
                modelBuilder.ApplyConfiguration(new DepositEntityConfiguration());
                modelBuilder.ApplyConfiguration(new LoQEntityConfig());
                modelBuilder.ApplyConfiguration(new QTEntityConfig());
                modelBuilder.ApplyConfiguration(new AwardEntityConfig());
                modelBuilder.ApplyConfiguration(new SkillEntityConfig());
                modelBuilder.ApplyConfiguration(new OrderCriteriaEntityConfig());
                modelBuilder.ApplyConfiguration(new OrderEntityConfig());
                modelBuilder.ApplyConfiguration(new PartnerEntityConfig());
                modelBuilder.ApplyConfiguration(new SponsorEntityConfig());
                modelBuilder.ApplyConfiguration(new SuffixEntityConfiguration());
                modelBuilder.ApplyConfiguration(new ApplicantFollowupStatusConfiguration());
                modelBuilder.ApplyConfiguration(new CompanyInformationEntityConfiguration());
                modelBuilder.ApplyConfiguration(new CountryOperationEntityConfiguration());
                modelBuilder.ApplyConfiguration(new ProcessDefinitionEntityConfig());
                modelBuilder.ApplyConfiguration(new ProcessEntityConfig());
                modelBuilder.ApplyConfiguration(new EnjazEntityConfig());
                modelBuilder.ApplyConfiguration(new ApplicantProcessEntityConfig());
                modelBuilder.ApplyConfiguration(new ComplaintEntityConfig());
                modelBuilder.ApplyConfiguration(new TicketReadyEntityConfig());
                modelBuilder.ApplyConfiguration(new TicketRegistrationEntityConfig());
                modelBuilder.ApplyConfiguration(new TicketRefundEntityConfig());
                modelBuilder.ApplyConfiguration(new TicketRebookEntityConfig());
                modelBuilder.ApplyConfiguration(new TicketRebookRegEntityConfig());
                modelBuilder.ApplyConfiguration(new TraveledApplicantEntityConfig());
                modelBuilder.ApplyConfiguration(new RequestedApplicantEntityConfig());
                modelBuilder.ApplyConfiguration(new LoginHistoryEntityConfig());
                modelBuilder.Ignore<PersonalInfo>();
            }
            #endregion
            base.OnModelCreating(modelBuilder);
            SeedData.Seedprocesses(modelBuilder);
            SeedData.SeedprocessDefinitions(modelBuilder);
            // SeedData.SeedPermissions(modelBuilder);

            #region Audit Config
            Audit.Core.Configuration.Setup()
                .UseEntityFramework(config => config
                .AuditTypeMapper(t => typeof(AuditLog))
                .AuditEntityAction<AuditLog>((auditEvent, auditedEntity, auditEntity) =>
                {
                    auditEntity.AuditData = JsonConvert.SerializeObject(auditedEntity, GetJsonSerializerSettings());
                    auditEntity.EntityType = auditedEntity.EntityType.Name;
                    auditEntity.AuditDate = DateTime.Now;
                    auditEntity.AuditUserId = Guid.NewGuid();

                    if (_userResolverService != null)
                    {
                        auditEntity.AuditUserId = _userResolverService.GetUserId();
                    }

                    auditEntity.Action = auditedEntity.Action;
                    auditEntity.Enviroment = JsonConvert.SerializeObject(new AuditEventEnvironment
                    {
                        UserName = auditEvent.Environment.UserName,
                        MachineName = auditEvent.Environment.MachineName,
                        DomainName = auditEvent.Environment.DomainName,
                        CallingMethodName = auditEvent.Environment.CallingMethodName,
                        Exception = auditEvent.Environment.Exception,
                        Culture = auditEvent.Environment.Culture
                    }, GetJsonSerializerSettings());

                    {
                        auditEntity.TablePk = auditedEntity.PrimaryKey.First().Value.ToString()!;
                    }
                }).IgnoreMatchedProperties(true));
            #endregion

        }
        public static JsonSerializerSettings GetJsonSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
                StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                Formatting = Formatting.Indented
            };
        }
        public string GetCurrentUserId()
        {
            return _userResolverService.GetUserId().ToString();
        }
    }
}
