

using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Domain.Entities.TicketData
{
    public class TicketRefund
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int DateInterval { get; set; }
        public Guid? ApplicantId { get; set; }
        public Applicant? Applicant { get; set; }
    }
}