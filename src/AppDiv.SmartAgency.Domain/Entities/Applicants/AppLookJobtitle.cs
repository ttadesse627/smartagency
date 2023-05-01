

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
public class AppLookJobtitle
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid? ApplicantJobtitleId { get; set; }
    public Applicant? ApplicantJobtitle { get; set; }
    public Guid? LookUpJobtitleId { get; set; }
    public LookUp? LookUpJobtitle { get; set; }
}