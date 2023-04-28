
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class LookUp : BaseAuditableEntity
    {
        public string? CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<Applicant>? ApplicantReligions { get; set; }
        public ICollection<Applicant>? ApplicantJobTitles { get; set; }
        public ICollection<Beneficiary>? BeneficiaryRelationShip { get; set; }
        public ICollection<Education>? LevelOfEducations { get; set; }
        public ICollection<Education>? QualificationTypeEducations { get; set; }
        public Education? AwardEducation { get; set; }
        public ICollection<Applicant>? TechnicalSkillApplicants { get; set; }
        public ICollection<Experience>? LookupExpeeriences { get; set; }
        public Language? LookupLanguage { get; set; }
        public string Value { get; set; } = string.Empty;

    }
}