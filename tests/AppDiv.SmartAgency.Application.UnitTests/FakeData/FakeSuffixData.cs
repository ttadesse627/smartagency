using AppDiv.SmartAgency.Application.Interfaces.Persistence.Settings;
using AppDiv.SmartAgency.Domain.Entities.Settings;

namespace AppDiv.SmartAgency.Test.FakeData
{
    public class FakeSuffixData
    {
        private readonly ISuffixRepository suffixRepository;

        public FakeSuffixData(ISuffixRepository suffixRepository)
        {
            this.suffixRepository = suffixRepository;
        }
        public async Task Create(CancellationToken cancellationToken)
        {
            List<Suffix> suffixes = new List<Suffix> {
                new Suffix
                {
                    Name="Mr.",
                    CreatedAt= DateTime.Now
                },
                new Suffix
                {
                    Name="Mrs.",
                    CreatedAt= DateTime.Now
                }
            };
            await suffixRepository.InsertAsync(suffixes, cancellationToken);
            await suffixRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
