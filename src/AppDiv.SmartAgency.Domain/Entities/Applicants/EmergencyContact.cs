
using System.ComponentModel.DataAnnotations.Schema;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
[Table("EmergencyContact")]
public class EmergencyContact : PersonalInfo
{
    public string Relationship { get; set; }
    public string AddressId { get; set; }
    public Address Address { get; set; }
    public string ApplicantId { get; set; }
    public Applicant Applicant { get; set; }
}