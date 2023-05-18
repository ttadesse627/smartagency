
using System.ComponentModel.DataAnnotations.Schema;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
[Table("EmergencyContacts")]
public class EmergencyContact : PersonalInfo
{
    public string? ArabicFullName { get; set; }
    
    // Foreign Keys
    public Guid? RelationshipId { get; set; }
    public Guid? ApplicantId { get; set; }
    public Guid? AddressId { get; set; }

    // Navigation properties
    public LookUp? Relationship { get; set; }
    public Applicant? Applicant { get; set; }
    public Address? Address { get; set; }
    
}