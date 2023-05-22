using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.Request.CompanyInformations
{
    public class LetterInformationRequest
    {
        public Guid? PartnerId { get; set; }
        public string? Agent { get; set; }
        public string? LetterLogo { get; set; }
        public string? LetterBackGround{ get; set; }
    }
}