

using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Domain.Entities.TicketData
{
    public class TraveledApplicant
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Remark { get; set; }
        public Guid? ApplicantProcessId { get; set; }
        public ApplicantProcess? ApplicantProcess { get; set; }
    }
}