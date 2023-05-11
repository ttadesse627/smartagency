using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.Request.Deposits
{
    public class CreateDepositRequest
    {
        public string PassportNumber {get; set;} 
        public double DepositAmount {get; set;} 
        public DateTime Month {get; set;}  
        public string DepositedBy {get; set;}
        
    }
}