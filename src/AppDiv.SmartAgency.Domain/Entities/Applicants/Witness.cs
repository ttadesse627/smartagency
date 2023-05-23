
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;

[Table("Witnesses")]
public class Witness : PersonalInfo
{
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public Guid? ApplicantId { get; set; }
    public Guid? CompanyInformationId { get; set; }

    public Applicant? Applicant { get; set; }
    public CompanyInformation? CompanyInformation { get; set; }

}