using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.DepositDTOs
{
    public class DepositResponseDTO
    {
        public Guid Id {get; set;}
        public string PassportNumber {get; set;} 
        public string DepositNumber {get; set;} 
        public string Month {get; set;}  
        public string DepositedBy {get; set;} 

    }
}