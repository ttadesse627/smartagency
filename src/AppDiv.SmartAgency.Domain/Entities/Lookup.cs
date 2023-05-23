
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Entities.Orders;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class LookUp : BaseAuditableEntity
    {
        public Guid? CategoryId { get; set; }
        public string Value { get; set; } = string.Empty;

        // Navigation properties
        public Category? Category { get; set; }
        public ICollection<Applicant>? ApplIssuingCountries { get; set; }
        public ICollection<Applicant>? ApplPassportIssuedPlaces { get; set; }
        public ICollection<Applicant>? ApplMaritalStatuses { get; set; }
        public ICollection<Applicant>? ApplHealthes { get; set; }
        public ICollection<Applicant>? ApplReligions { get; set; }
        public ICollection<Applicant>? ApplJobtitles { get; set; }
        public ICollection<Applicant>? ApplExperiences { get; set; }
        public ICollection<Applicant>? ApplLanguages { get; set; }
        public ICollection<Applicant>? ApplSalaries { get; set; }
        public ICollection<Applicant>? ApplDesiredCountries { get; set; }
        public ICollection<Applicant>? ApplBrokerNames { get; set; }
        public ICollection<Applicant>? ApplBranches { get; set; }
        public ICollection<Skill>? Skills { get; set; }
        public ICollection<LanguageSkill>? LanguageSkills { get; set; }
        public ICollection<Experience>? ExpCountries { get; set; }
        public ICollection<QualificationType>? QualificationTypes { get; set; }
        public ICollection<LevelOfQualification>? LevelOfQualifications { get; set; }
        public ICollection<Award>? Awards { get; set; }
        public ICollection<EmergencyContact>? ECRelationships { get; set; }
        public ICollection<Beneficiary>? BenRelationShips { get; set; }
        public ICollection<Address>? AddressRegions { get; set; }

        public ICollection<OnlineApplicant>? MaritalStatus { get; set; }
        public ICollection<OnlineApplicant>? Experience { get; set; }
        public ICollection<ApplicantFollowupStatus>? FollowupStatus { get; set; }
        public ICollection<OnlineApplicant>? OnlineApplDesiredCountries { get; set; }

        public ICollection<Order>? LookUpPortOfArrivals { get; set; }
        public ICollection<Order>? LookUpPriorities { get; set; }
        public ICollection<Order>? LookUpVisaTypes { get; set; }

        public ICollection<OrderCriteria>? LookUpCriteriaNationalities { get; set; }
        public ICollection<OrderCriteria>? LookUpCriteriaJobTitles { get; set; }
        public ICollection<OrderCriteria>? LookUpCriteriaSalaries { get; set; }
        public ICollection<OrderCriteria>? LookUpCriteriaReligions { get; set; }
        public ICollection<OrderCriteria>? LookUpCriteriaExperiences { get; set; }
        public ICollection<OrderCriteria>? LookUpCriteriaLanguages { get; set; }
         public ICollection<Address>? Countries { get; set; }
         public CountryOperation? CountryOperation { get; set; }
    }
}