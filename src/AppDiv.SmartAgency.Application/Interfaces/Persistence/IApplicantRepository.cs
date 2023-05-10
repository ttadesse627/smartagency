

using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities.Applicants;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence;
public interface IApplicantRepository : IBaseRepository<Applicant>
{
      Task<ServiceResponse<Applicant>> GetApplicantByPassportNumber(string passportNumber);
}