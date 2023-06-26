

using AppDiv.SmartAgency.Domain.Entities.Applicants;

namespace AppDiv.SmartAgency.Domain.Entities.TicketData
{
    public class TicketReady
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? DateInterval { get; set; }
        public Guid? TicketOfficeId { get; set; }
        public Guid? ApplicantId { get; set; }
        public Applicant? Applicant { get; set; }
        public LookUp? TicketOffice { get; set; }
    }
}