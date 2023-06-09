
using System.ComponentModel.DataAnnotations.Schema;
using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
public class EmergencyContact : BaseAuditableEntity
{
    public string? NameOfContactPerson { get; set; }
    public string? ArabicName { get; set; }
    public DateTime? BirthDate { get; set; }
    public Gender? Gender { get; set; }
    public Guid? RelationshipId { get; set; }

    public Guid? ApplicantId { get; set; }
    public Guid? AddressId { get; set; }

    // Navigation properties
    public LookUp? Relationship { get; set; }
    public Applicant? Applicant { get; set; }
    public Address? Address { get; set; }

}