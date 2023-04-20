using AppDiv.CRVS.Domain.Entities;
using AppDiv.CRVS.Domain.Entities.Audit;
using AppDiv.CRVS.Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.CRVS.Infrastructure.Context
{
    public interface ICRVSDbContext
    {
        Guid GetCurrentUserId();
    }
}
