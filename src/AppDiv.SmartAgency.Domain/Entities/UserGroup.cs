using AppDiv.SmartAgency.Domain.Base;
using AppDiv.SmartAgency.Domain.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Domain.Entities
{
    // Customer entity 
    public class UserGroup : BaseAuditableEntity
    {
      public string GroupName { get; set; }
    }
}
