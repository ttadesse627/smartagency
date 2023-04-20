using AppDiv.CRVS.Domain.Base;
using AppDiv.CRVS.Domain.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.CRVS.Domain.Entities
{
    // Customer entity 
    public class UserGroup : BaseAuditableEntity
    {
      public string GroupName { get; set; }
    }
}
