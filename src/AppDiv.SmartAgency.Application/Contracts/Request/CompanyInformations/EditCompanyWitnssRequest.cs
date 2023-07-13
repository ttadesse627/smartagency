using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.Request.CompanyInformations
{
    public class EditCompanyWitnssRequest
    {
        public ICollection<EditCompanyWitnessRequest>? Witnesses { get; set; }
    }
}