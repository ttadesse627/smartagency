using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.PageDTOs
{
    public class PageResponseDTO
    {
    
      public string  Category {get; set;}  
      public string  Link {get; set;}
      public string  Title{get; set;}
      public string  PageContent{get; set;}

      public DateTime CreatedAt {get; set;}

         
    }
}