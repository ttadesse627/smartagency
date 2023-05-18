

using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities.Orders;
public class OrderCriteria
{
    public Guid Id { get; set; }
    public int? Age { get; set; }
    public string? Remark { get; set; }

    // Foreign Keys
    public Guid? NationalityId { get; set; }
    public Guid? JobTitleId { get; set; }
    public Guid? SalaryId { get; set; }
    public Guid? ReligionId { get; set; }
    public Guid? ExperienceId { get; set; }
    public Guid? LanguageId { get; set; }
    public Guid? OrderId { get; set; }
    
    // Navigation properties
    public LookUp? Nationality { get; set; }
    public LookUp? JobTitle { get; set; }
    public LookUp? Salary { get; set; }
    public LookUp? Religion { get; set; }
    public LookUp? Experience { get; set; }
    public LookUp? Language { get; set; }
    public Order? Order { get; set; }

}