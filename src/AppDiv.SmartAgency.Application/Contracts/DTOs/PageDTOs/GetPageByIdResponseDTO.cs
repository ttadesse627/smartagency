using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.PageDTOs
{
    public class GetPageByIdResponseDTO : PageResponseDTO
    {
         public Guid Id {get; set;}
    }
}