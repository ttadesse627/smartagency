using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class ApplicantFollowupStatus : BaseAuditableEntity
    {
        public string PassportNumber { get; set; }
        public DateTime Month { get; set; }
        public string? Remark { get; set; }

        public Guid FollowupStatusId { get; set; }
        public Guid ApplicantId { get; set; }
        public Applicant? Applicant { get; set; }
        public LookUp? FollowupStatus { get; set; }
    }
}