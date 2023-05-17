using System.ComponentModel.DataAnnotations.Schema;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;

[Table("Repersentative")]
public class Representative : PersonalInfo
{
    public Guid? RepresentativeAddressId { get; set; }
    public Guid? RepresentativeApplicantId { get; set; }
    public Address? RepresentativeAddress { get; set; }
    public Applicant? RepresentativeApplicant { get; set; }
}