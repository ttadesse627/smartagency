using System.ComponentModel.DataAnnotations.Schema;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;

[Table("Repersentative")]
public class Repersentative : PersonalInfo
{
    public Guid AddressId { get; set; }
    public Address Address { get; set; }
    public ICollection<Applicant> RepresentativeApplicants { get; set; }
}