

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
public class Beneficiary : PersonalInfo
{
    public string? Region { get; set; }
    public string? Zone { get; set; }
    public string? Woreda { get; set; }
    public string RelationshipId { get; set; }
    public LookUp? Relationship { get; set; }
    public float Rate { get; set; }
}