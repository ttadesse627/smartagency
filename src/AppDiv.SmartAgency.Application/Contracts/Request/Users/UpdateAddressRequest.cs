using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;

namespace AppDiv.SmartAgency.Application.Contracts.Request.UserRequests;

public class UpdateAddressRequest
{
    public Guid Id { get; set; }
    public Guid CountryId { get; set; }
    public Guid AddressRegionId { get; set; }
    public string Zone { get; set; }
    public string Woreda { get; set; }
    public string Kebele { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
}