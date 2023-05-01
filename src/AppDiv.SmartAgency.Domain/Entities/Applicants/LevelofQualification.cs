

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
public class LevelOfQualification
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid? LevelOfQualificationLookUpId { get; set; }
    public LookUp? LevelOfQualificationLookUp { get; set; }
    public Guid? LevelOfQualificationEducationId { get; set; }
    public Education? LevelOfQualificationEducation { get; set; }
}