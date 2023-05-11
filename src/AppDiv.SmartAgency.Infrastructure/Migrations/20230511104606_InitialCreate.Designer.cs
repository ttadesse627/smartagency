﻿
#nullable disable

using AppDiv.SmartAgency.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    [DbContext(typeof(SmartAgencyDbContext))]
    [Migration("20230511104606_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Applicants.Applicant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("AmharicFullName")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("ApplicantAddressId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ApplicantBankAccountId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ApplicantBranchId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ApplicantBrokerNameId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ApplicantDesiredCountryId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ApplicantHealthId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ApplicantIssuedPlaceId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ApplicantIssuingCountryId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ApplicantJobtitleId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ApplicantMaritalStatusId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ApplicantPartnerId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ApplicantReligionId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ApplicantSalaryId")
                        .HasColumnType("char(36)");

                    b.Property<string>("ArabicFullName")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Complexion")
                        .HasColumnType("longtext");

                    b.Property<int>("ContractPeriod")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("CurrentNationality")
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<decimal>("Height")
                        .HasColumnType("decimal(65,30)");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsRequested")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsReserved")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("IssuedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("IssuingCountry")
                        .HasColumnType("longtext");

                    b.Property<string>("JobTitleAmharic")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("MotherFullName")
                        .HasColumnType("longtext");

                    b.Property<int>("NumberOfChildren")
                        .HasColumnType("int");

                    b.Property<DateTime>("PassportExpiryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PassportNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("PlaceOfBirth")
                        .HasColumnType("longtext");

                    b.Property<string>("PreviousCountry")
                        .HasColumnType("longtext");

                    b.Property<string>("Remark")
                        .HasColumnType("longtext");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantAddressId")
                        .IsUnique();

                    b.HasIndex("ApplicantBankAccountId")
                        .IsUnique();

                    b.HasIndex("ApplicantBranchId");

                    b.HasIndex("ApplicantBrokerNameId");

                    b.HasIndex("ApplicantDesiredCountryId");

                    b.HasIndex("ApplicantHealthId");

                    b.HasIndex("ApplicantIssuedPlaceId");

                    b.HasIndex("ApplicantIssuingCountryId");

                    b.HasIndex("ApplicantJobtitleId");

                    b.HasIndex("ApplicantMaritalStatusId");

                    b.HasIndex("ApplicantPartnerId");

                    b.HasIndex("ApplicantReligionId");

                    b.HasIndex("ApplicantSalaryId");

                    b.ToTable("Applicant");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Applicants.BankAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<long>("AccountNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("BankName")
                        .HasColumnType("longtext");

                    b.Property<string>("BranchName")
                        .HasColumnType("longtext");

                    b.Property<string>("SwiftCode")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Applicants.Beneficiary", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("BeneficiaryApplicantId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("BeneficiaryRelationshipId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<float>("Rate")
                        .HasColumnType("float");

                    b.Property<string>("Region")
                        .HasColumnType("longtext");

                    b.Property<string>("Woreda")
                        .HasColumnType("longtext");

                    b.Property<string>("Zone")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("BeneficiaryApplicantId");

                    b.HasIndex("BeneficiaryRelationshipId");

                    b.ToTable("Beneficiary");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Applicants.Education", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("EducationApplicantId")
                        .HasColumnType("char(36)");

                    b.Property<string>("FieldOfStudy")
                        .HasColumnType("longtext");

                    b.Property<string>("ProfessionalSkill")
                        .HasColumnType("longtext");

                    b.Property<int>("YearCompleted")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EducationApplicantId")
                        .IsUnique();

                    b.ToTable("Educations");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Applicants.EmergencyContact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ArabicFullName")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("EmergencyContactAddressId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("EmergencyContactApplicantId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("EmergencyContactRegionId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("EmergencyContactRelationshipId")
                        .HasColumnType("char(36)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("EmergencyContactAddressId")
                        .IsUnique();

                    b.HasIndex("EmergencyContactApplicantId")
                        .IsUnique();

                    b.HasIndex("EmergencyContactRegionId");

                    b.HasIndex("EmergencyContactRelationshipId");

                    b.ToTable("EmergencyContact");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Applicants.Experience", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ExperienceApplicantId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ExperienceCountryId")
                        .HasColumnType("char(36)");

                    b.Property<int>("PeriodLength")
                        .HasColumnType("int");

                    b.Property<string>("Position")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ExperienceApplicantId");

                    b.HasIndex("ExperienceCountryId");

                    b.ToTable("Experiences");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Applicants.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("CanListen")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("CanRead")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("CanSpeak")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("CanWrite")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("LanguageApplicantId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("LanguageLookUpId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Proficiency")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LanguageApplicantId");

                    b.HasIndex("LanguageLookUpId");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Applicants.Repersentative", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("RepersentativeAddressId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("RepresentativeApplicantId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RepersentativeAddressId")
                        .IsUnique();

                    b.HasIndex("RepresentativeApplicantId")
                        .IsUnique();

                    b.ToTable("Repersentative");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Applicants.Witness", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Witness");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PersonalInfoId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserGroupId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Attachment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<bool>("ShowOnCv")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Audit.AuditLog", b =>
                {
                    b.Property<Guid>("AuditId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("AuditData")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("AuditDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("AuditUserId")
                        .HasColumnType("char(36)");

                    b.Property<string>("EntityType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Enviroment")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TablePk")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("AuditId");

                    b.ToTable("AuditLogs");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Base.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("AddressCountryId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("AddressEmergContactId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Kebele")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("PartnerId")
                        .HasColumnType("char(36)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("Region")
                        .HasColumnType("longtext");

                    b.Property<string>("Website")
                        .HasColumnType("longtext");

                    b.Property<string>("Woreda")
                        .HasColumnType("longtext");

                    b.Property<string>("Zone")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AddressCountryId");

                    b.HasIndex("AddressEmergContactId");

                    b.HasIndex("PartnerId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Base.FileCollection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("FileCollectionApplicantId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("FileCollectionAttachmentId")
                        .HasColumnType("char(36)");

                    b.Property<string>("FilePath")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("FileCollectionApplicantId");

                    b.HasIndex("FileCollectionAttachmentId");

                    b.ToTable("FileCollections");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8aec3c2a-96ba-46ce-8a4b-14cf557fd621"),
                            Name = "Category"
                        });
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("char(36)");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("longtext")
                        .HasDefaultValue("");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("longtext")
                        .HasDefaultValue("");

                    b.Property<Guid?>("SuffixId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("SuffixId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Deposit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ApplicantId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<double>("DepositAmount")
                        .HasColumnType("double");

                    b.Property<string>("DepositedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Month")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PassportNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.ToTable("Deposits");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.LookUp", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("LookupExpeeriencesId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("LookupLanguageId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("LookupExpeeriencesId");

                    b.HasIndex("LookupLanguageId");

                    b.ToTable("LookUps");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.OnlineApplicant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Age")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<Guid>("DesiredCountryId")
                        .HasColumnType("char(36)");

                    b.Property<string>("EducationLevel")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("ExperienceId")
                        .HasColumnType("char(36)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("MaritalStatusId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Passport")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DesiredCountryId");

                    b.HasIndex("ExperienceId");

                    b.HasIndex("MaritalStatusId");

                    b.ToTable("OnlineApplicants");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Orders.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("ContractDuration")
                        .HasColumnType("int");

                    b.Property<string>("ContractNumber")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ElectronicVisaDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ElectronicVisaNumber")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("OrderCriteriaId")
                        .HasColumnType("char(36)");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("OrderPaymentId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("OrderSponsorId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("PartnerId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("PortOfArrivalId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("PriorityId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("VisaDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("VisaFileId")
                        .HasColumnType("char(36)");

                    b.Property<string>("VisaNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("VisaTypeId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.HasIndex("OrderCriteriaId")
                        .IsUnique();

                    b.HasIndex("OrderPaymentId")
                        .IsUnique();

                    b.HasIndex("OrderSponsorId")
                        .IsUnique();

                    b.HasIndex("PartnerId");

                    b.HasIndex("PortOfArrivalId");

                    b.HasIndex("PriorityId");

                    b.HasIndex("VisaFileId")
                        .IsUnique();

                    b.HasIndex("VisaTypeId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Orders.OrderCriteria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<Guid?>("ExperienceId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("LanguageId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("NationalityId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("OrderCriteriaJobTitleId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ReligionId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Remark")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("SalaryId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ExperienceId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("NationalityId");

                    b.HasIndex("OrderCriteriaJobTitleId");

                    b.HasIndex("ReligionId");

                    b.HasIndex("SalaryId");

                    b.ToTable("OrderCriterias");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Orders.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("CurrentPaidAmount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("PaidAmount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("OrderPayments");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Orders.Sponsor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FullNameAmharic")
                        .HasColumnType("longtext");

                    b.Property<string>("FullNameArabic")
                        .HasColumnType("longtext");

                    b.Property<string>("IdNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("NumberOfFamily")
                        .HasColumnType("int");

                    b.Property<string>("OtherName")
                        .HasColumnType("longtext");

                    b.Property<string>("ResidentialTitle")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("SponsorAddressId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("SponsorIDFileId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("SponsorAddressId")
                        .IsUnique();

                    b.HasIndex("SponsorIDFileId")
                        .IsUnique();

                    b.ToTable("Sponsors");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Partner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("BankAccount")
                        .HasColumnType("longtext");

                    b.Property<string>("BankName")
                        .HasColumnType("longtext");

                    b.Property<string>("ContactPerson")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("HeaderLogo")
                        .HasColumnType("longtext");

                    b.Property<string>("IdNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LicenseNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("ManagerNameAmharic")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("PartnerAddressId")
                        .HasColumnType("char(36)");

                    b.Property<string>("PartnerName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PartnerNameAmharic")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PartnerNameArabic")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PartnerType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ReferenceNumber")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("PartnerAddressId")
                        .IsUnique();

                    b.ToTable("Partners");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Settings.Suffix", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2023, 5, 11, 13, 46, 5, 360, DateTimeKind.Local).AddTicks(2787));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "IDX_UQ_Suffix")
                        .IsUnique();

                    b.ToTable("Suffixes");
                });

            modelBuilder.Entity("ApplicantLookUp", b =>
                {
                    b.Property<Guid>("ApplicantTechnicalSkillsId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("LookupTechnicalSkillsId")
                        .HasColumnType("char(36)");

                    b.HasKey("ApplicantTechnicalSkillsId", "LookupTechnicalSkillsId");

                    b.HasIndex("LookupTechnicalSkillsId");

                    b.ToTable("ApplicantLookUp");
                });

            modelBuilder.Entity("ApplicantWitness", b =>
                {
                    b.Property<Guid>("ApplicantWitnessesId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("WitnessApplicantsId")
                        .HasColumnType("char(36)");

                    b.HasKey("ApplicantWitnessesId", "WitnessApplicantsId");

                    b.HasIndex("WitnessApplicantsId");

                    b.ToTable("ApplicantWitness");
                });

            modelBuilder.Entity("EducationLookUp", b =>
                {
                    b.Property<Guid>("EducationLevelofQualificationsId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("LookUpLevelOfQualificationsId")
                        .HasColumnType("char(36)");

                    b.HasKey("EducationLevelofQualificationsId", "LookUpLevelOfQualificationsId");

                    b.HasIndex("LookUpLevelOfQualificationsId");

                    b.ToTable("EducationLookUp");
                });

            modelBuilder.Entity("EducationLookUp1", b =>
                {
                    b.Property<Guid>("EducationQualificationTypesId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("LookUpQualificationTypesId")
                        .HasColumnType("char(36)");

                    b.HasKey("EducationQualificationTypesId", "LookUpQualificationTypesId");

                    b.HasIndex("LookUpQualificationTypesId");

                    b.ToTable("EducationLookUp1");
                });

            modelBuilder.Entity("EducationLookUp2", b =>
                {
                    b.Property<Guid>("EducationAwardsId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("LookUpAwardsId")
                        .HasColumnType("char(36)");

                    b.HasKey("EducationAwardsId", "LookUpAwardsId");

                    b.HasIndex("LookUpAwardsId");

                    b.ToTable("EducationLookUp2");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Applicants.Applicant", b =>
                {
                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Base.Address", "ApplicantAddress")
                        .WithOne("AddressApplicant")
                        .HasForeignKey("AppDiv.SmartAgency.Domain.Entities.Applicants.Applicant", "ApplicantAddressId");

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Applicants.BankAccount", "ApplicantBankAccount")
                        .WithOne("Applicant")
                        .HasForeignKey("AppDiv.SmartAgency.Domain.Entities.Applicants.Applicant", "ApplicantBankAccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "ApplicantBranch")
                        .WithMany("LookUpBranches")
                        .HasForeignKey("ApplicantBranchId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "ApplicantBrokerName")
                        .WithMany("LookUpBrokerNames")
                        .HasForeignKey("ApplicantBrokerNameId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "ApplicantDesiredCountry")
                        .WithMany("LookUpDesiredCountries")
                        .HasForeignKey("ApplicantDesiredCountryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "ApplicantHealth")
                        .WithMany("LookUpHealths")
                        .HasForeignKey("ApplicantHealthId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "ApplicantIssuedPlace")
                        .WithMany("LookUpIssuedPlaces")
                        .HasForeignKey("ApplicantIssuedPlaceId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "ApplicantIssuingCountry")
                        .WithMany("LookUpIssuingCountries")
                        .HasForeignKey("ApplicantIssuingCountryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "ApplicantJobtitle")
                        .WithMany("LookUpJobTitles")
                        .HasForeignKey("ApplicantJobtitleId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "ApplicantMaritalStatus")
                        .WithMany("LookUpMaritalStatuses")
                        .HasForeignKey("ApplicantMaritalStatusId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Partner", "ApplicantPartner")
                        .WithMany("Applicants")
                        .HasForeignKey("ApplicantPartnerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "ApplicantReligion")
                        .WithMany("LookUpReligions")
                        .HasForeignKey("ApplicantReligionId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "ApplicantSalary")
                        .WithMany("LookUpSalaries")
                        .HasForeignKey("ApplicantSalaryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("ApplicantAddress");

                    b.Navigation("ApplicantBankAccount");

                    b.Navigation("ApplicantBranch");

                    b.Navigation("ApplicantBrokerName");

                    b.Navigation("ApplicantDesiredCountry");

                    b.Navigation("ApplicantHealth");

                    b.Navigation("ApplicantIssuedPlace");

                    b.Navigation("ApplicantIssuingCountry");

                    b.Navigation("ApplicantJobtitle");

                    b.Navigation("ApplicantMaritalStatus");

                    b.Navigation("ApplicantPartner");

                    b.Navigation("ApplicantReligion");

                    b.Navigation("ApplicantSalary");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Applicants.Beneficiary", b =>
                {
                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Applicants.Applicant", "BeneficiaryApplicant")
                        .WithMany("ApplicantBeneficiaries")
                        .HasForeignKey("BeneficiaryApplicantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "BeneficiaryRelationship")
                        .WithMany("BeneficiaryRelationShip")
                        .HasForeignKey("BeneficiaryRelationshipId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("BeneficiaryApplicant");

                    b.Navigation("BeneficiaryRelationship");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Applicants.Education", b =>
                {
                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Applicants.Applicant", "EducationApplicant")
                        .WithOne("ApplicantEducation")
                        .HasForeignKey("AppDiv.SmartAgency.Domain.Entities.Applicants.Education", "EducationApplicantId");

                    b.Navigation("EducationApplicant");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Applicants.EmergencyContact", b =>
                {
                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Base.Address", "EmergencyContactAddress")
                        .WithOne("AddressEmergencyContact")
                        .HasForeignKey("AppDiv.SmartAgency.Domain.Entities.Applicants.EmergencyContact", "EmergencyContactAddressId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Applicants.Applicant", "EmergencyContactApplicant")
                        .WithOne("ApplicantEmergencyContact")
                        .HasForeignKey("AppDiv.SmartAgency.Domain.Entities.Applicants.EmergencyContact", "EmergencyContactApplicantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "EmergencyContactRegion")
                        .WithMany("LookUpEmergencyContactRegions")
                        .HasForeignKey("EmergencyContactRegionId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "EmergencyContactRelationship")
                        .WithMany("LookUpEmergencyContactRelationships")
                        .HasForeignKey("EmergencyContactRelationshipId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("EmergencyContactAddress");

                    b.Navigation("EmergencyContactApplicant");

                    b.Navigation("EmergencyContactRegion");

                    b.Navigation("EmergencyContactRelationship");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Applicants.Experience", b =>
                {
                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Applicants.Applicant", "ExperienceApplicant")
                        .WithMany("ApplicantExperiences")
                        .HasForeignKey("ExperienceApplicantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "ExperienceCountry")
                        .WithMany("LookUpExperiences")
                        .HasForeignKey("ExperienceCountryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("ExperienceApplicant");

                    b.Navigation("ExperienceCountry");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Applicants.Language", b =>
                {
                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Applicants.Applicant", "LanguageApplicant")
                        .WithMany("ApplicantLanguages")
                        .HasForeignKey("LanguageApplicantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "LanguageLookUp")
                        .WithMany("LookupLanguages")
                        .HasForeignKey("LanguageLookUpId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("LanguageApplicant");

                    b.Navigation("LanguageLookUp");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Applicants.Repersentative", b =>
                {
                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Base.Address", "RepersentativeAddress")
                        .WithOne("AddressRepresentative")
                        .HasForeignKey("AppDiv.SmartAgency.Domain.Entities.Applicants.Repersentative", "RepersentativeAddressId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Applicants.Applicant", "RepresentativeApplicant")
                        .WithOne("ApplicantRepersentative")
                        .HasForeignKey("AppDiv.SmartAgency.Domain.Entities.Applicants.Repersentative", "RepresentativeApplicantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("RepersentativeAddress");

                    b.Navigation("RepresentativeApplicant");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Base.Address", b =>
                {
                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "AddressCountry")
                        .WithMany("LookUpAddressCountries")
                        .HasForeignKey("AddressCountryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Applicants.EmergencyContact", "AddressEmergContact")
                        .WithMany()
                        .HasForeignKey("AddressEmergContactId");

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Partner", "Partner")
                        .WithMany()
                        .HasForeignKey("PartnerId");

                    b.Navigation("AddressCountry");

                    b.Navigation("AddressEmergContact");

                    b.Navigation("Partner");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Base.FileCollection", b =>
                {
                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Applicants.Applicant", "FileCollectionApplicant")
                        .WithMany("ApplicantFileCollections")
                        .HasForeignKey("FileCollectionApplicantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Attachment", "FileCollectionAttachment")
                        .WithMany("FileCollections")
                        .HasForeignKey("FileCollectionAttachmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("FileCollectionApplicant");

                    b.Navigation("FileCollectionAttachment");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Customer", b =>
                {
                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Base.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Settings.Suffix", "Suffix")
                        .WithMany()
                        .HasForeignKey("SuffixId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Address");

                    b.Navigation("Suffix");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Deposit", b =>
                {
                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Applicants.Applicant", "Applicant")
                        .WithMany("Deposits")
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Applicant");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.LookUp", b =>
                {
                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Category", "Category")
                        .WithMany("LookUps")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Applicants.Experience", "LookupExpeeriences")
                        .WithMany()
                        .HasForeignKey("LookupExpeeriencesId");

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Applicants.Language", "LookupLanguage")
                        .WithMany()
                        .HasForeignKey("LookupLanguageId");

                    b.Navigation("Category");

                    b.Navigation("LookupExpeeriences");

                    b.Navigation("LookupLanguage");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.OnlineApplicant", b =>
                {
                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "DesiredCountry")
                        .WithMany("DesiredCountry")
                        .HasForeignKey("DesiredCountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "Experience")
                        .WithMany("Experience")
                        .HasForeignKey("ExperienceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "MaritalStatus")
                        .WithMany("MaritalStatus")
                        .HasForeignKey("MaritalStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DesiredCountry");

                    b.Navigation("Experience");

                    b.Navigation("MaritalStatus");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Orders.Order", b =>
                {
                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Applicants.Applicant", "Employee")
                        .WithOne("ApplicantOrder")
                        .HasForeignKey("AppDiv.SmartAgency.Domain.Entities.Orders.Order", "EmployeeId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Orders.OrderCriteria", "OrderCriteria")
                        .WithOne("Order")
                        .HasForeignKey("AppDiv.SmartAgency.Domain.Entities.Orders.Order", "OrderCriteriaId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Orders.Payment", "OrderPayment")
                        .WithOne("Order")
                        .HasForeignKey("AppDiv.SmartAgency.Domain.Entities.Orders.Order", "OrderPaymentId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Orders.Sponsor", "OrderSponsor")
                        .WithOne("SponsorOrder")
                        .HasForeignKey("AppDiv.SmartAgency.Domain.Entities.Orders.Order", "OrderSponsorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Partner", "Partner")
                        .WithMany("Orders")
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "PortOfArrival")
                        .WithMany("LookUpPortOfArrivals")
                        .HasForeignKey("PortOfArrivalId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "Priority")
                        .WithMany("LookUpPriorities")
                        .HasForeignKey("PriorityId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Base.FileCollection", "VisaFile")
                        .WithOne("FileCollectionOrder")
                        .HasForeignKey("AppDiv.SmartAgency.Domain.Entities.Orders.Order", "VisaFileId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "VisaType")
                        .WithMany("LookUpVisaTypes")
                        .HasForeignKey("VisaTypeId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Employee");

                    b.Navigation("OrderCriteria");

                    b.Navigation("OrderPayment");

                    b.Navigation("OrderSponsor");

                    b.Navigation("Partner");

                    b.Navigation("PortOfArrival");

                    b.Navigation("Priority");

                    b.Navigation("VisaFile");

                    b.Navigation("VisaType");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Orders.OrderCriteria", b =>
                {
                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "Experience")
                        .WithMany("LookUpCriteriaExperiences")
                        .HasForeignKey("ExperienceId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "Language")
                        .WithMany("LookUpCriteriaLanguages")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "Nationality")
                        .WithMany("LookUpCriteriaNationalities")
                        .HasForeignKey("NationalityId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "OrderCriteriaJobTitle")
                        .WithMany("LookUpCriteriaJobTitles")
                        .HasForeignKey("OrderCriteriaJobTitleId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "Religion")
                        .WithMany("LookUpCriteriaReligions")
                        .HasForeignKey("ReligionId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", "Salary")
                        .WithMany("LookUpCriteriaSalaries")
                        .HasForeignKey("SalaryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Experience");

                    b.Navigation("Language");

                    b.Navigation("Nationality");

                    b.Navigation("OrderCriteriaJobTitle");

                    b.Navigation("Religion");

                    b.Navigation("Salary");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Orders.Sponsor", b =>
                {
                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Base.Address", "SponsorAddress")
                        .WithOne("AddressSponsor")
                        .HasForeignKey("AppDiv.SmartAgency.Domain.Entities.Orders.Sponsor", "SponsorAddressId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Base.FileCollection", "SponsorIDFile")
                        .WithOne("FileCollectionSponsor")
                        .HasForeignKey("AppDiv.SmartAgency.Domain.Entities.Orders.Sponsor", "SponsorIDFileId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("SponsorAddress");

                    b.Navigation("SponsorIDFile");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Partner", b =>
                {
                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Base.Address", "PartnerAddress")
                        .WithOne("AddressPartner")
                        .HasForeignKey("AppDiv.SmartAgency.Domain.Entities.Partner", "PartnerAddressId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("PartnerAddress");
                });

            modelBuilder.Entity("ApplicantLookUp", b =>
                {
                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", null)
                        .WithMany()
                        .HasForeignKey("ApplicantTechnicalSkillsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Applicants.Applicant", null)
                        .WithMany()
                        .HasForeignKey("LookupTechnicalSkillsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApplicantWitness", b =>
                {
                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Applicants.Witness", null)
                        .WithMany()
                        .HasForeignKey("ApplicantWitnessesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Applicants.Applicant", null)
                        .WithMany()
                        .HasForeignKey("WitnessApplicantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EducationLookUp", b =>
                {
                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", null)
                        .WithMany()
                        .HasForeignKey("EducationLevelofQualificationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Applicants.Education", null)
                        .WithMany()
                        .HasForeignKey("LookUpLevelOfQualificationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EducationLookUp1", b =>
                {
                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", null)
                        .WithMany()
                        .HasForeignKey("EducationQualificationTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Applicants.Education", null)
                        .WithMany()
                        .HasForeignKey("LookUpQualificationTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EducationLookUp2", b =>
                {
                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.LookUp", null)
                        .WithMany()
                        .HasForeignKey("EducationAwardsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.Applicants.Education", null)
                        .WithMany()
                        .HasForeignKey("LookUpAwardsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("AppDiv.SmartAgency.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Applicants.Applicant", b =>
                {
                    b.Navigation("ApplicantBeneficiaries");

                    b.Navigation("ApplicantEducation");

                    b.Navigation("ApplicantEmergencyContact");

                    b.Navigation("ApplicantExperiences");

                    b.Navigation("ApplicantFileCollections");

                    b.Navigation("ApplicantLanguages");

                    b.Navigation("ApplicantOrder");

                    b.Navigation("ApplicantRepersentative");

                    b.Navigation("Deposits");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Applicants.BankAccount", b =>
                {
                    b.Navigation("Applicant");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Attachment", b =>
                {
                    b.Navigation("FileCollections");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Base.Address", b =>
                {
                    b.Navigation("AddressApplicant");

                    b.Navigation("AddressEmergencyContact");

                    b.Navigation("AddressPartner");

                    b.Navigation("AddressRepresentative");

                    b.Navigation("AddressSponsor");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Base.FileCollection", b =>
                {
                    b.Navigation("FileCollectionOrder");

                    b.Navigation("FileCollectionSponsor");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Category", b =>
                {
                    b.Navigation("LookUps");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.LookUp", b =>
                {
                    b.Navigation("BeneficiaryRelationShip");

                    b.Navigation("DesiredCountry");

                    b.Navigation("Experience");

                    b.Navigation("LookUpAddressCountries");

                    b.Navigation("LookUpBranches");

                    b.Navigation("LookUpBrokerNames");

                    b.Navigation("LookUpCriteriaExperiences");

                    b.Navigation("LookUpCriteriaJobTitles");

                    b.Navigation("LookUpCriteriaLanguages");

                    b.Navigation("LookUpCriteriaNationalities");

                    b.Navigation("LookUpCriteriaReligions");

                    b.Navigation("LookUpCriteriaSalaries");

                    b.Navigation("LookUpDesiredCountries");

                    b.Navigation("LookUpEmergencyContactRegions");

                    b.Navigation("LookUpEmergencyContactRelationships");

                    b.Navigation("LookUpExperiences");

                    b.Navigation("LookUpHealths");

                    b.Navigation("LookUpIssuedPlaces");

                    b.Navigation("LookUpIssuingCountries");

                    b.Navigation("LookUpJobTitles");

                    b.Navigation("LookUpMaritalStatuses");

                    b.Navigation("LookUpPortOfArrivals");

                    b.Navigation("LookUpPriorities");

                    b.Navigation("LookUpReligions");

                    b.Navigation("LookUpSalaries");

                    b.Navigation("LookUpVisaTypes");

                    b.Navigation("LookupLanguages");

                    b.Navigation("MaritalStatus");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Orders.OrderCriteria", b =>
                {
                    b.Navigation("Order");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Orders.Payment", b =>
                {
                    b.Navigation("Order");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Orders.Sponsor", b =>
                {
                    b.Navigation("SponsorOrder");
                });

            modelBuilder.Entity("AppDiv.SmartAgency.Domain.Entities.Partner", b =>
                {
                    b.Navigation("Applicants");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
