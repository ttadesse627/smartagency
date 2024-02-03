

using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Domain.Entities;
public class ApplicantProcess : BaseAuditableEntity
{
    public Guid ProcessDefinitionId { get; set; }
    public Guid ApplicantId { get; set; }
    public DateTime Date { get; set; }
    public ProcessStatus Status { get; set; }
    public ProcessDefinition ProcessDefinition { get; set; } = null!;
    public Applicant Applicant { get; set; } = null!;
}