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

        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Suffix> Suffixes { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<LookUp> LookUps { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<AttachmentFile> AttachmentFiles { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<LanguageSkill> LanguageSkills { get; set; }
        public DbSet<Representative> Representatives { get; set; }
        public DbSet<Witness> Witnesses { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<QualificationType> QualificationTypes { get; set; }
        public DbSet<LevelOfQualification> LevelOfQualifications { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<OnlineApplicant> OnlineApplicants { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<OrderCriteria> OrderCriterias { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<ApplicantFollowupStatus> ApplicantFollowupStatuses { get; set; }
        public DbSet<CompanyInformation> CompanyInformations { get; set; }
        public DbSet<CompanySetting> CompanySettings { get; set; }
        public DbSet<CountryOperation> CountryOperations { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<ProcessDefinition> ProcessDefinitions { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Enjaz> Enjazs { get; set; }
        public DbSet<ApplicantProcess> ApplicantProcesses { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<TicketReady> TicketReadies { get; set; }
        public DbSet<TicketRegistration> TicketRegistrations { get; set; }
        public DbSet<TicketRefund> TicketRefunds { get; set; }
        public DbSet<TicketRebook> TicketRebooks { get; set; }
        public DbSet<TicketRebookReg> TicketRebookRegistrations { get; set; }
        public DbSet<TraveledApplicant> TraveledApplicants { get; set; }
        public DbSet<dynamic> partner_view { get; set; }

   /* protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<dynamic>().HasNoKey().ToView("DynamicView");
    }
*/
        public SmartAgencyDbContext(DbContextOptions<SmartAgencyDbContext> options, IUserResolverService userResolverService) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
            _userResolverService = userResolverService;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // var dbContext = new SmartAgencyDbContext(optionsBuilder.Options);
            // To run sql scripts, example alter database to set collation, create stored procedure, function, view ....
            // optionsBuilder.ReplaceService<IMigrationsSqlGenerator, CustomSqlServerMigrationsSqlGenerator>();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Entity Configuration
            {
                modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
                modelBuilder.ApplyConfiguration(new AddressEntityConfig());
                modelBuilder.ApplyConfiguration(new ApplicantEntityConfig());
                modelBuilder.ApplyConfiguration(new AttachmentFileEntityConfig());
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
                modelBuilder.Ignore<PersonalInfo>();
            }
            #endregion
            base.OnModelCreating(modelBuilder);
            SeedData.Seedprocesses(modelBuilder);
            SeedData.SeedprocessDefinitions(modelBuilder);

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
                    /*if (auditedEntity.EntityType == typeof(test))
                    {
                        var json = auditEntity.AuditData.Replace("'", "\"");
                        var jo = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(json);
                        var referenceNo = jo["ColumnValues"]["ReferenceNumber"];
                        auditEntity.TablePk = jo["ColumnValues"]["ReferenceNumber"].ToString();
                    }*/
                    //else
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
                DateFormatString = "yyyy-MM-dd hh:mm:ss",
                StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                Formatting = Newtonsoft.Json.Formatting.Indented
            };
        }

        public string GetCurrentUserId()
        {
            return _userResolverService.GetUserId().ToString();
        }
    }
}
