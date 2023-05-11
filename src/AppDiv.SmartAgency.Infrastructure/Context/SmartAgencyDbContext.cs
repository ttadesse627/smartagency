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

namespace AppDiv.SmartAgency.Infrastructure.Context
{
    public class SmartAgencyDbContext : AuditIdentityDbContext<ApplicationUser>
    {
        // private readonly IUserResolverService userResolverService;

        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Suffix> Suffixes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<LookUp> LookUps { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<FileCollection> FileCollections { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Repersentative> Repersentatives { get; set; }
        public DbSet<Witness> Witnesses { get; set; }
        public DbSet<OnlineApplicant> OnlineApplicants { get; set; }
         public DbSet<Deposit>  Deposits{ get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<OrderCriteria> OrderCriterias { get; set; }
        public DbSet<Payment> OrderPayments { get; set; }


        public SmartAgencyDbContext(DbContextOptions<SmartAgencyDbContext> options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
            // this.userResolverService = userResolverService;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            // To run sql scripts, example alter database to set collation, create stored procedure, function, view ....
            // optionsBuilder.ReplaceService<IMigrationsSqlGenerator, CustomSqlServerMigrationsSqlGenerator>();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Entity Configuration
            {
                //  modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
                modelBuilder.ApplyConfiguration(new AddressEntityConfig());
                modelBuilder.ApplyConfiguration(new ApplicantEntityConfig());
                modelBuilder.ApplyConfiguration(new BeneficiaryEntityConfig());
                modelBuilder.ApplyConfiguration(new CustomerEntityConfiguration());
                modelBuilder.ApplyConfiguration(new EducationEntityConfig());
                modelBuilder.ApplyConfiguration(new EmergencyContactEntityConfig());
                modelBuilder.ApplyConfiguration(new ExperienceEntityConfig());
                modelBuilder.ApplyConfiguration(new FileCollectionEntityConfig());
                modelBuilder.ApplyConfiguration(new LanguageEntityConfig());
                modelBuilder.ApplyConfiguration(new CustomerEntityConfiguration());
                modelBuilder.ApplyConfiguration(new OnlineApplicantEntityConfiguration());
                modelBuilder.ApplyConfiguration(new DepositEntityConfiguration());
                modelBuilder.ApplyConfiguration(new LookUpEntityConfiguration());
                modelBuilder.ApplyConfiguration(new OrderCriteriaEntityConfig());
                modelBuilder.ApplyConfiguration(new OrderEntityConfig());
                modelBuilder.ApplyConfiguration(new PartnerEntityConfig());
                modelBuilder.ApplyConfiguration(new RepresentativeEntityConfig());
                modelBuilder.ApplyConfiguration(new SponsorEntityConfig());
                modelBuilder.ApplyConfiguration(new SuffixEntityConfiguration());

                modelBuilder.Entity<Category>().HasData(
                    new Category { Id = Guid.Parse("8aec3c2a-96ba-46ce-8a4b-14cf557fd621"), Name = "Category" }
                );

                modelBuilder.Ignore<PersonalInfo>();

                // modelBuilder.Entity<Applicant>(entity =>
                // {
                //     entity.Property().HasConversion(
                //         x => JsonConvert.SerializeObject(x) //convert TO a json string
                //         , x => JsonConvert.DeserializeObject<BaseModel>(x)//convert FROM a json string
                //     );
                // });

            }
            #endregion
            base.OnModelCreating(modelBuilder);
            // SeedData.SeedRoles(modelBuilder);
            // SeedData.SeedUsers(modelBuilder);
            // SeedData.SeedUserRoles(modelBuilder);
            // SeedData.SeedGender(modelBuilder);
            // SeedData.SeedSuffix(modelBuilder);

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
                    // if (userResolverService != null)
                    // {
                    //     auditEntity.AuditUserId = userResolverService.GetUserId();
                    // }

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

        // public string GetCurrentUserId()
        // {
        //     return userResolverService.GetUserId();
        // }
    }
}
