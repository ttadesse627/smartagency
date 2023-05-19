using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class Deposit :BaseAuditableEntity
    {
        public string PassportNumber{get; set;}
        public double DepositAmount{get; set;}
        public DateTime Month{get; set;}
        public string DepositedBy{get; set;}

        public Guid ApplicantId{get; set;}
        public Applicant? Applicant{get; set;}

    }
}