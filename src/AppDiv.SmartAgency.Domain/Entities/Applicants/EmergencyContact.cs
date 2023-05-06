
using System.ComponentModel.DataAnnotations.Schema;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
[Table("EmergencyContact")]
public class EmergencyContact : PersonalInfo
{
    // Foreign Keys
    public Guid? EmergencyContactAddressId { get; set; }
    public Guid? EmergencyContactApplicantId { get; set; }
    public Guid EmergencyContactRelationshipId { get; set; }

    // Navigation properties
    public LookUp? EmergencyContactRelationship { get; set; }
    public Address? EmergencyContactAddress { get; set; }
    public Applicant? EmergencyContactApplicant { get; set; }
}