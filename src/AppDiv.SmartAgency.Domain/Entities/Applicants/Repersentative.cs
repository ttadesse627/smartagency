using System.ComponentModel.DataAnnotations.Schema;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;

[Table("Repersentative")]
public class Repersentative : PersonalInfo
{
    public Guid? RepersentativeAddressId { get; set; }
    public Guid? RepresentativeApplicantId { get; set; }
    public Address? RepersentativeAddress { get; set; }
    public Applicant? RepresentativeApplicant { get; set; }
}