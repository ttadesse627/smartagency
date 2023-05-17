

using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities.Orders;
public class OrderCriteria
{
    public Guid Id { get; set; }
    public int? Age { get; set; }
    public string? Remark { get; set; }

    // Foreign Keys
    public Guid? OrderCriteriaNationalityId { get; set; }
    public Guid? OrderCriteriaJobTitleId { get; set; }
    public Guid? OrderCriteriaSalaryId { get; set; }
    public Guid? OrderCriteriaReligionId { get; set; }
    public Guid? OrderCriteriaExperienceId { get; set; }
    public Guid? OrderCriteriaLanguageId { get; set; }
    public Guid? OrderCriteriaOrderId { get; set; }
    
    // Navigation properties
    public LookUp? OrderCriteriaNationality { get; set; }
    public LookUp? OrderCriteriaJobTitle { get; set; }
    public LookUp? OrderCriteriaSalary { get; set; }
    public LookUp? OrderCriteriaReligion { get; set; }
    public LookUp? OrderCriteriaExperience { get; set; }
    public LookUp? OrderCriteriaLanguage { get; set; }
    public Order? OrderCriteriaOrder { get; set; }

}