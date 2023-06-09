using System.ComponentModel.DataAnnotations.Schema;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
public class Beneficiary : BaseAuditableEntity
{
    public string? FullName { get; set; }
    public string? Region { get; set; }
    public string? Zone { get; set; }
    public string? Woreda { get; set; }
    public float Rate { get; set; }

    // Foreign Keys
    public Guid? RelationshipId { get; set; }
    public Guid? ApplicantId { get; set; }

    // Navigation properties
    public LookUp? Relationship { get; set; }
    public Applicant? Applicant { get; set; }

}