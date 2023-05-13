
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;

[Table("Witnesses")]
public class Witness : PersonalInfo
{
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public ICollection<Applicant>? WitnessApplicants { get; set; }
}