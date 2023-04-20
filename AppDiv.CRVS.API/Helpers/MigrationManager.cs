using AppDiv.CRVS.Infrastructure;
using AppDiv.CRVS.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AppDiv.CRVS.API.Helpers
{
    public static class MigrationManager
    {
        public static WebApplication MigrateDatabase(this WebApplication webApp)
        {
            using (var scope = webApp.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<CRVSDbContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                }
            }

            return webApp;
        }
    }
}