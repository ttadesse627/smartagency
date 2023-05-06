
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class LookUp : BaseAuditableEntity
    {
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<Applicant>? ApplicantReligions { get; set; }
        public ICollection<AppLookJobtitle>? LookUpJobTitles { get; set; }
        public ICollection<Beneficiary>? BeneficiaryRelationShip { get; set; }
        public ICollection<OnlineApplicant>? MaritalStatus { get; set; }
        public ICollection<OnlineApplicant>? Experience { get; set; }

        public ICollection<OnlineApplicant>? DesiredCountry { get; set; }
        public LevelOfQualification? LookUpLevelOfQualifications { get; set; }
        public QualificationType? LookUpQualificationTypes { get; set; }
        public Award? AwardEducations { get; set; }
        public TechnicalSkill? LookupTechnicalSkill { get; set; }
        public Experience? LookupExpeeriences { get; set; }
        public Language? LookupLanguage { get; set; }
        public string Value { get; set; } = string.Empty;
    }
}