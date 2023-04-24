using AppDiv.SmartAgency.Domain.Configuration.Settings;
using AppDiv.SmartAgency.Domain.Entities.Audit;
using AppDiv.SmartAgency.Domain.Entities.Settings;
using AppDiv.SmartAgency.Infrastructure.Context;
using Audit.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain;
using AppDiv.SmartAgency.Infrastructure.Seed;
using Audit.EntityFramework;
using AppDiv.SmartAgency.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AppDiv.SmartAgency.Infrastructure.Context
{
    public class SmartAgencyDbContext : AuditIdentityDbContext<ApplicationUser>, ISmartAgencyDbContext
{
    private readonly IUserResolverService userResolverService;

    public DbSet<AuditLog> AuditLogs { get; set; }
    // public DbSet<Gender> Genders { get; set; }
    public DbSet<Suffix> Suffixes { get; set; }
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Attachment> Attachments => Set<Attachment>();
    public SmartAgencyDbContext(DbContextOptions<SmartAgencyDbContext> options, IUserResolverService userResolverService) : base(options)
    {
        this.ChangeTracker.LazyLoadingEnabled = false;
        this.userResolverService = userResolverService;
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
            // modelBuilder.ApplyConfiguration(new GenderEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SuffixEntityConfiguration());
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = Guid.NewGuid().ToString(), Name = "Country" },
                new Category { Id = Guid.NewGuid().ToString(), Name = "Qualification Type" },
                new Category { Id = Guid.NewGuid().ToString(), Name = "Language" },
                new Category { Id = Guid.NewGuid().ToString(), Name = "Award" },
                new Category { Id = Guid.NewGuid().ToString(), Name = "Skill" }
            );

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
                auditEntity.AuditUserId = Guid.NewGuid().ToString();
                if (userResolverService != null)
                {
                    auditEntity.AuditUserId = userResolverService.GetUserId();
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
                    auditEntity.TablePk = auditedEntity.PrimaryKey.First().Value.ToString();
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
        return userResolverService.GetUserId();
    }
}
}
