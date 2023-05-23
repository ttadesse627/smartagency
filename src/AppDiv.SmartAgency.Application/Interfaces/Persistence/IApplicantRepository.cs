

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Features.Applicants.Command.Update;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities.Applicants;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence;
public interface IApplicantRepository : IBaseRepository<Applicant>
{
    public Task<Int32> CreateApplicantAsync(Applicant applicant, CancellationToken cancellationToken);
    public Task<Applicant> GetApplicantByIdAsync(Guid id);
    public Task<List<Applicant>> GetAll();
    public Task<ServiceResponse<Int32>> DeleteApplicantAsync();
    public Task<int> EditApplicantAsync(Applicant applicant);
    public Task<ServiceResponse<Int32>> SaveDbUpdateAsync();
    public Task<ServiceResponse<Applicant>> GetApplicantByPassportNumber(string passportNumber);
    public Task<int> AddApplicantAsync(Applicant applicant);
    public Task<Applicant> GetApplicantByIdWithAsync(Guid id);
}