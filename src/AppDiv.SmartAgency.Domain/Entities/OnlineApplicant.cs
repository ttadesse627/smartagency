using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class OnlineApplicant : BaseAuditableEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string Passport { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public string Age { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string EducationLevel { get; set; } = string.Empty;
        public Guid DesiredCountryId { get; set; }
        public Guid MaritalStatusId { get; set; }
        public Guid ExperienceId { get; set; }
        public LookUp? MaritalStatus { get; set; }
        public LookUp? Experience { get; set; }
        public LookUp? DesiredCountry { get; set; }
    }
}