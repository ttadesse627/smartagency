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
        public double DepositAmount {get; set;} 
        public DateTime Month {get; set;}  
        public string DepositedBy {get; set;} 
       public DepositApplicantResponseDTO Applicant {get; set;}


    }
}