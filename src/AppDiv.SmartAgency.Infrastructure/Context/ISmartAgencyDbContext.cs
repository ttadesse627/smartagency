using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Audit;
using AppDiv.SmartAgency.Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Infrastructure.Context
{
    public interface ISmartAgencyDbContext
    {
        Guid GetCurrentUserId();
    }
}
