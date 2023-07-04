

using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Entities.TicketData;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Domain.Entities;
public class ApplicantProcess : BaseAuditableEntity
{
    public Guid? ProcessDefinitionId { get; set; }
    public Guid? ApplicantId { get; set; }
    public DateTime Date { get; set; }
    public ProcessStatus? Status { get; set; }
    public ProcessDefinition? ProcessDefinition { get; set; }
    public Applicant? Applicant { get; set; }
    // public TicketReady? TicketReady { get; set; }
    // public TicketRegistration? TicketRegistration { get; set; }
    // public TicketRefund? TicketRefund { get; set; }
    // public TicketRebook? TicketRebook { get; set; }
    // public TicketRebookReg? TicketRebookReg { get; set; }
    // public TraveledApplicant? TraveledApplicant { get; set; }
}