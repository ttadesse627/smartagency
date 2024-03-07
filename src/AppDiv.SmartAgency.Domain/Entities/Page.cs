using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities
{
    public class Page : BaseAuditableEntity
    {
        public string? Category { get; set; }
        public string? Link { get; set; }
        public string? Title { get; set; }
        public string? PageContent { get; set; }
    }

}