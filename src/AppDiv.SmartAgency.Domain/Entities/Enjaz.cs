
using AppDiv.SmartAgency.Domain.Entities.Orders;
using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Entities.Applicants;

namespace AppDiv.SmartAgency.Domain.Entities;
public class Enjaz : BaseAuditableEntity
{
    public string ApplicationNumber { get; set; } = null!;
    public string TransactionCode { get; set; } = null!;
    public Guid ApplicantId { get; set; }
    public Applicant Applicant { get; set; } = null!;
}