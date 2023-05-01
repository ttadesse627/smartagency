using System.ComponentModel.DataAnnotations.Schema;


namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
[Table("Beneficiary")]
public class Beneficiary : PersonalInfo
{
    public string? Region { get; set; }
    public string? Zone { get; set; }
    public string? Woreda { get; set; }
    public Guid RelationshipId { get; set; }
    public LookUp? Relationship { get; set; }
    public Guid ApplicantId { get; set; }
    public Applicant Applicant { get; set; }
    public float Rate { get; set; }
}