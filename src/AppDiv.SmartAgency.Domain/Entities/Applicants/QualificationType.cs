

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
public class QualificationType
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid? QualificationTypeEducationId { get; set; }
    public Education? QualificationTypeEducation { get; set; }
    public Guid? QualificationTypeLookUpId { get; set; }
    public LookUp? QualificationTypeLookUp { get; set; }
}