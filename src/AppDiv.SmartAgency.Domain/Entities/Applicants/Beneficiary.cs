using System.ComponentModel.DataAnnotations.Schema;


namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
[Table("Beneficiary")]
public class Beneficiary : PersonalInfo
{
    public string? Region { get; set; }
    public string? Zone { get; set; }
    public string? Woreda { get; set; }
    public float Rate { get; set; }

    // Foreign Keys
    public Guid? BeneficiaryApplicantId { get; set; }
    public Guid? BeneficiaryRelationshipId { get; set; }

    // Navigation properties
    public LookUp? BeneficiaryRelationship { get; set; }
    public Applicant? BeneficiaryApplicant { get; set; }
    
}