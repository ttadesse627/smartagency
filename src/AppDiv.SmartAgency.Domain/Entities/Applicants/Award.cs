

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
public class Award
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid? AwardEducationId { get; set; }
    public Education? AwardEducation { get; set; }
    public Guid? AwardLookUpId { get; set; }
    public LookUp? AwardLookUp { get; set; }
}