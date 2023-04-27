
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class LookUp : BaseAuditableEntity
    {
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Applicant>? ApplicantReligions { get; set; }
        public ICollection<Applicant>? ApplicantJobTitles { get; set; }
        public Beneficiary? BeneficiaryRelationShip { get; set; }
        public Education? LevelOfEducation { get; set; }
        public Education? QualificationType { get; set; }
        public ICollection<Education>? Awards { get; set; }
        public ICollection<Applicant>? TechnicalSkillApplicants { get; set; }
        public ICollection<Experience>? LookupExpeeriences { get; set; }
        public string Value { get; set; } = string.Empty;

    }
}