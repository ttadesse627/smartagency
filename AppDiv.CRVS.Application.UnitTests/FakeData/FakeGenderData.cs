

using AppDiv.CRVS.Domain.Entities.Settings;

namespace AppDiv.CRVS.Test.FakeData
{
    public class FakeGenderData
    {
        private readonly IGenderRepository genderRepository;

        public FakeGenderData(IGenderRepository genderRepository)
        {
            this.genderRepository = genderRepository;
        }
        public async Task Create(CancellationToken cancellationToken)
        {
            List<Gender> genders = new List<Gender> {
                new Gender
                {
                    Name="Male",
                    CreatedAt= DateTime.Now
                },
                new Gender
                {
                    Name="Female",
                    CreatedAt= DateTime.Now
                }
            };
            await genderRepository.InsertAsync(genders, cancellationToken);
            await genderRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
