

using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities;
public class Complaint : BaseAuditableEntity
{
    public string Message { get; set; } = null!;
    public Guid ApplicantId { get; set; }
    public bool IsClosed { get; set; } = false;
    public ApplicationUser User { get; set; } = null!;
    public Applicant Applicant { get; set; } = null!;
}