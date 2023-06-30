
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class CountryOperation : BaseAuditableEntity
    {
        public Guid? CountryId {get; set;}
        public string LicenseNumber {get; set;}
        public int VisaExpiryDays {get; set;}
        public Guid? CompanyInformationId {get; set;}

        public LookUp? LookUpCountryOperation {get; set;}
        public CompanyInformation? CompanyInformation {get; set;}

        
    }
}