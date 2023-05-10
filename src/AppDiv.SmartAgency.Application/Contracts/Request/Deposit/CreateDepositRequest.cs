using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Deposit
{
    public class CreateDepositRequest
    {
        
        public string PassportNumber {get; set;} 
        public string DepositNumber {get; set;} 
        public string Month {get; set;}  
        public string DepositedBy {get; set;} 

    }
}