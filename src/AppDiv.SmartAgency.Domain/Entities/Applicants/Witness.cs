
using System.ComponentModel.DataAnnotations.Schema;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;

public class Witness : BaseAuditableEntity
{
    public string? FullName { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public Guid? ApplicantId { get; set; }
    public Guid? CompanyInformationId { get; set; }
    public Applicant? Applicant { get; set; }
    public CompanyInformation? CompanyInformation { get; set; }

}