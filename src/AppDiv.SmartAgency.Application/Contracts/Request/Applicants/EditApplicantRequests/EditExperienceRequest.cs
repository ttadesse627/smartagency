

namespace AppDiv.SmartAgency.Application.Contracts.Request.Applicants. EditApplicantRequests;
public class EditExperienceRequest
{
    public Guid Id { get; set; }
    public int PeriodLength { get; set; }
    public string? Position { get; set; }
    public Guid? CountryId { get; set; }
}