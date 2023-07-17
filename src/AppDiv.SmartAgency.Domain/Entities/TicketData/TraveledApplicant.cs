

using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities.TicketData
{
    public class TraveledApplicant : BaseAuditableEntity
    {
        //public Guid Id { get; set; } = Guid.NewGuid();
        public string? Remark { get; set; }
        public Guid? ApplicantId { get; set; }
        public Applicant? Applicant { get; set; }
    }
}