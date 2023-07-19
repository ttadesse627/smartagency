using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.QuickLinksDTOs
{
    public class NotProcessedApplicantResponseDTO
    {

        // public int  FileNo {get; set;}

        // public ICollection<string> Columns { get; set; } = new str
        public string PassportNumber { get; set; }
        public int Duration { get; set; }
        public string FullName { get; set; }
        public string Sex { get; set; }
        public string MaritalStatus { get; set; }
        public string Religion { get; set; }
        public string Profession { get; set; }
        public string ArabicName { get; set; }
        public int Age { get; set; }
        // public string Status {get; set;}

    }
}