using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AppDiv.SmartAgency.Infrastructure.Persistence
{
    public class LoginHistoryRepository : BaseRepository<LoginHistory>, ILoginHistoryRepository
    {
        private readonly SmartAgencyDbContext _context;
        public LoginHistoryRepository(SmartAgencyDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

    }
}