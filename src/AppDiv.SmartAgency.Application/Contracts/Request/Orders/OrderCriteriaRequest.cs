

namespace AppDiv.SmartAgency.Application.Contracts.Request.Orders;
public record OrderCriteriaRequest
{
    public int? Age { get; set; }
    public string? Remark { get; set; }
    public Guid? OrderCriteriaNationalityId { get; set; }
    public Guid? OrderCriteriaJobTitleId { get; set; }
    public Guid? OrderCriteriaSalaryId { get; set; }
    public Guid? OrderCriteriaReligionId { get; set; }
    public Guid? OrderCriteriaExperienceId { get; set; }
    public Guid? OrderCriteriaLanguageId { get; set; }
}