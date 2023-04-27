
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities.Applicants;
public class EmergencyContact : PersonalInfo
{
    public string Relationship { get; set; }
    public Address Address { get; set; }
}