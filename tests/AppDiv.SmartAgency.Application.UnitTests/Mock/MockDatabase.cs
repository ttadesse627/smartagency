using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;

namespace AppDiv.SmartAgency.Test.Mock
{
    public class MockDatabase
    {
        private readonly DbContextOptions<SmartAgencyDbContext> dbContextOptions = new DbContextOptionsBuilder<SmartAgencyDbContext>()
                .UseInMemoryDatabase("smartagency", new InMemoryDatabaseRoot())
                .Options;
        public SmartAgencyDbContext CreateDbContext()
        {
            Mock<IUserResolverService> userResolverService = new Mock<IUserResolverService>();
            userResolverService.Setup(x => x.GetUserId()).Returns(new string("b74ddd14-6340-4840-95c2-db12554843e5"));
            userResolverService.Setup(x => x.GetUserEmail()).Returns("tagele@gmail.com");
            userResolverService.Setup(x => x.GetLocale()).Returns("en-us");
            SmartAgencyDbContext dbContext = new SmartAgencyDbContext(dbContextOptions, userResolverService.Object);
            return dbContext;
        }
    }
}
