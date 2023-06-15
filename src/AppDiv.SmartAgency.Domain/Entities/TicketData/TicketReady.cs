

using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Domain.Entities.TicketData
{
    public class TicketReady
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? DateInterval { get; set; }
        public Guid? TicketOfficeId { get; set; }
        public Guid? ApplicantProcessId { get; set; }
        public ApplicantProcess? ApplicantProcess { get; set; }
        public LookUp? TicketOffice { get; set; }
    }
}