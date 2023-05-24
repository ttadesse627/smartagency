

namespace AppDiv.SmartAgency.Application.Contracts.Request.Orders;
public record EditOrderCriteriaRequest
{
    public Guid Id { get; set; }
    public Guid? NationalityId { get; set; }
    public Guid? JobTitleId { get; set; }
    public Guid? SalaryId { get; set; }
    public Guid? ReligionId { get; set; }
    public Guid? ExperienceId { get; set; }
    public Guid? LanguageId { get; set; }
    public int? Age { get; set; }
    public string? Remark { get; set; }
}