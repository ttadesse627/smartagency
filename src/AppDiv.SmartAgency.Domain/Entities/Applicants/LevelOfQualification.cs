namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
public class LevelOfQualification
{
    public Guid Id { get; set; }
    public Guid? LookUpId { get; set; }
    public Guid? EducationId { get; set; }
    public Education? Education { get; set; }
    public LookUp? LookUp { get; set; }
}