
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class LookUp : BaseAuditableEntity
    {
        public Guid? CategoryId { get; set; }
        public string Value { get; set; } = string.Empty;

        // Navigation properties
        public Category? Category { get; set; }
        public ICollection<Applicant>? LookUpReligions { get; set; }
        public ICollection<Applicant>? LookUpJobTitles { get; set; }
        public ICollection<Applicant>? LookupTechnicalSkills { get; set; }
        public ICollection<Applicant>? LookUpBrokerNames { get; set; }
        public ICollection<Applicant>? LookUpBranches { get; set; }

        public ICollection<Education>? LookUpLevelOfQualifications { get; set; }
        public ICollection<Education>? LookUpQualificationTypes { get; set; }
        public ICollection<Education>? LookUpAwards { get; set; }
        
        public ICollection<Experience>? LookUpExperiences { get; set; }
        public ICollection<Beneficiary>? BeneficiaryRelationShip { get; set; }
        public ICollection<Language>? LookupLanguages { get; set; }
    }
}