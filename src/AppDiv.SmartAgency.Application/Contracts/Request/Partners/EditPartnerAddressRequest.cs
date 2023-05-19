using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Partners
{
    public class EditPartnerAddressRequest: PartnerAddressRequest
    {
         public Guid? Id { get; set; }
        
    }
}