

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Features.Command.Update.Applicants;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities.Applicants;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence;
public interface IApplicantRepository : IBaseRepository<Applicant>
{
    public Task<Int32> CreateApplicantAsync(Applicant applicant);
    public Task<Applicant> GetApplicantAsync(Guid id);
    public Task<ServiceResponse<List<Applicant>>> GetAll();
    public Task<ServiceResponse<Int32>> EditApplicantAsync();
}