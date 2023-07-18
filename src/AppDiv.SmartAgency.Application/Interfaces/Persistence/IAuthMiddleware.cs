using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence
{
    public interface IAuthMiddleware
    {

        public Task InvokeAsync(HttpContext context);

    }
}