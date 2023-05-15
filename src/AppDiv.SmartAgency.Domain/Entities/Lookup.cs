
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Entities.Orders;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class LookUp : BaseAuditableEntity
    {
        public Guid CategoryId { get; set; }
        public string Value { get; set; } = string.Empty;

        // Navigation properties
        public Category Category { get; set; }
        public ICollection<Applicant>? LookUpReligions { get; set; }
        public ICollection<Applicant>? LookUpJobTitles { get; set; }
        public ICollection<Applicant>? LookupTechnicalSkills { get; set; }
        public ICollection<Applicant>? LookUpBrokerNames { get; set; }
        public ICollection<Applicant>? LookUpBranches { get; set; }
        public ICollection<Applicant>? LookUpIssuingCountries { get; set; }
        public ICollection<Applicant>? LookUpIssuedPlaces { get; set; }
        public ICollection<Applicant>? LookUpHealths { get; set; }
        public ICollection<Applicant>? LookUpSalaries { get; set; }
        public ICollection<Applicant>? LookUpDesiredCountries { get; set; }
        public ICollection<Applicant>? LookUpMaritalStatuses { get; set; }

        public ICollection<Education>? LookUpLevelOfQualifications { get; set; }
        public ICollection<Education>? LookUpQualificationTypes { get; set; }
        public ICollection<Education>? LookUpAwards { get; set; }

        public ICollection<Experience>? LookUpExperiences { get; set; }
        public ICollection<Beneficiary>? BeneficiaryRelationShip { get; set; }

        public ICollection<OnlineApplicant>? MaritalStatus { get; set; }
        public ICollection<OnlineApplicant>? Experience { get; set; }

        public ICollection<ApplicantFollowupStatus>?  FollowupStatus{ get; set; }
        public ICollection<OnlineApplicant>? DesiredCountry { get; set; }
        public ICollection<Language>? LookUpLanguages { get; set; }
        public ICollection<EmergencyContact>? LookUpEmergencyContactRelationships { get; set; }
        public ICollection<Address>? LookUpAddressRegions { get; set; }

        public ICollection<Order>? LookUpPortOfArrivals { get; set; }
        public ICollection<Order>? LookUpPriorities { get; set; }
        public ICollection<Order>? LookUpVisaTypes { get; set; }

        public ICollection<OrderCriteria>? LookUpCriteriaNationalities { get; set; }
        public ICollection<OrderCriteria>? LookUpCriteriaJobTitles { get; set; }
        public ICollection<OrderCriteria>? LookUpCriteriaSalaries { get; set; }
        public ICollection<OrderCriteria>? LookUpCriteriaReligions { get; set; }
        public ICollection<OrderCriteria>? LookUpCriteriaExperiences { get; set; }
        public ICollection<OrderCriteria>? LookUpCriteriaLanguages { get; set; }
        //public ICollection<Education>? LookUpExpriences { get; internal set; }
    }
}