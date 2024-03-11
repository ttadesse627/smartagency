using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class CompanySetting : BaseAuditableEntity
    {
        public int FileNumberStartFrom { get; set; }
        public int PrintedDocumentSubmitDays { get; set; }
        public int AmountOfDeposit { get; set; }
        public string? AuthorizedPerson { get; set; }
        public int PenalityInterval { get; set; }
        public int PenalityAmount { get; set; }

        public Guid? CompanyInformationId { get; set; }

        public CompanyInformation? CompanyInformation { get; set; }


    }
}