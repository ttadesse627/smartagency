

using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Domain.Entities.TicketData
{
    public class TicketRebook
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int DateInterval { get; set; }
        public Guid? ApplicantProcessId { get; set; }
        public ApplicantProcess? ApplicantProcess { get; set; }
    }
}