using System.ComponentModel.DataAnnotations.Schema;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;

[Table("Repersentative")]
public class Representative : PersonalInfo
{
    public Guid? AddressId { get; set; }
    public Guid? ApplicantId { get; set; }
    public Address? Address { get; set; }
    public Applicant? Applicant { get; set; }
}