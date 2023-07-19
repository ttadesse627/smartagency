using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs
{
    public class TravelledApplicantsResponseDTO
    {
        public string? PartnerName { get; set; }
        public string? OrderNo { get; set; }
        public int Days { get; set; }
        public string? EnjazNumber { get; set; }
        public string? OrderCode { get; set; }
        public string? VisaNumber { get; set; }
        public string? Employee { get; set; }
        public string? PassportNo { get; set; }
        public string? Priority { get; set; }
        public decimal RemainedPayment { get; set; }
        public string? JobTitle { get; set; }
        public string? Salary { get; set; }


    }
}