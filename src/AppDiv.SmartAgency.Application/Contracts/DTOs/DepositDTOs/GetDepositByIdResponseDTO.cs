using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.Request.Deposits;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.DepositDTOs
{
    public class GetDepositByIdResponseDTO : CreateDepositRequest
    {
        public Guid Id {get; set;} 
        public Guid ApplicantId {get; set;} 
    }
}