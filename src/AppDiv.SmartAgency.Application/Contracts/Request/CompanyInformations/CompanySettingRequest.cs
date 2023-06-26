using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.Request.CompanyInformations
{
    public class CompanySettingRequest
    {
        public int FileNumberStartFrom { get; set; }
        public int PrintedDocumentSubmitDays { get; set; }
        public int AmountOfDeposit { get; set; }
        public string? AuthorizedPerson { get; set; }

    }
}