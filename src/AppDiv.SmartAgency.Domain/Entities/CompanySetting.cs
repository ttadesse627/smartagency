using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class CompanySetting :BaseAuditableEntity
    {
        public int FileNumberStartFrom {get; set;}=0;
        public int PrintedDocumentSubmitDays {get; set;}=2;
        public int AmountOfDeposit {get; set;}
        public bool ViseManager {get; set;}=false;
        public bool Manager {get; set;}=true;

        public Guid? CompanyInformationId {get; set;}

        public CompanyInformation? CompanyInformation {get; set;}
         

    }
}