

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Features.Applicants.Command.Update;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities.Applicants;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence;
public interface IApplicantRepository : IBaseRepository<Applicant>
{
    public Task<Int32> CreateApplicantAsync(Applicant applicant, CancellationToken cancellationToken);
    public Task<Applicant> GetApplicantAsync(Guid id);
    public Task<List<Applicant>> GetAll();
    public Task<ServiceResponse<Int32>> EditApplicantAsync();
    public Task<ServiceResponse<Applicant>> GetApplicantByPassportNumber(string passportNumber);
}