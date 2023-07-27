using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.Request.Orders;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Partners
{
    public class EditPartnerAddressRequest : SponsorAddressRequest
    {
        public Guid? Id { get; set; }

    }
}