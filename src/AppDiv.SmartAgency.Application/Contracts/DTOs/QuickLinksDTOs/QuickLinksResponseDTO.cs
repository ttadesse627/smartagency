using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.QuickLinksDTOs
{
    public class QuickLinksResponseDTO
    {
        public string? Name { get; set; }
        public int Count { get; set; }
        public string? Path { get; set; }
    }
}