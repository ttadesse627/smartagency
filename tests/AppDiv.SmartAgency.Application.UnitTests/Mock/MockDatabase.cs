using AppDiv.CRVS.Application.Interfaces.Persistence;
using AppDiv.CRVS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;

namespace AppDiv.CRVS.Test.Mock
{
    public class MockDatabase
    {
        private readonly DbContextOptions<CRVSDbContext> dbContextOptions = new DbContextOptionsBuilder<CRVSDbContext>()
                .UseInMemoryDatabase("CRVSDB", new InMemoryDatabaseRoot())
                .Options;
        public CRVSDbContext CreateDbContext()
        {
            Mock<IUserResolverService> userResolverService = new Mock<IUserResolverService>();
            userResolverService.Setup(x => x.GetUserId()).Returns(new Guid("b74ddd14-6340-4840-95c2-db12554843e5"));
            userResolverService.Setup(x => x.GetUserEmail()).Returns("tagele@gmail.com");
            userResolverService.Setup(x => x.GetLocale()).Returns("en-us");
            CRVSDbContext dbContext = new CRVSDbContext(dbContextOptions, userResolverService.Object);
            return dbContext;
        }
    }
}
