using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class LetterInformation: BaseAuditableEntity
    {
        
        public Guid? PartnerId { get; set; }
        public string? Agent { get; set; }
        public string? LetterLogo { get; set; }
        public string? LetterBackGround{ get; set; }
        public Guid? CompanyInformationId{ get; set; }

         public CompanyInformation? CompanyInformation{ get; set; }
        public Partner? Partner { get; set; }


    }
}