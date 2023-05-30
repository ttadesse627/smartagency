

using System.Linq.Expressions;
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Utility.Contracts;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence;
public interface IApplicantRepository : IBaseRepository<Applicant>
{
    public Task<Applicant> GetApplicantByIdAsync(Guid id);
    public Task<List<Applicant>> GetAll();
    public Task<int> EditApplicantAsync(Applicant applicant);
    public Task<ServiceResponse<Applicant>> GetApplicantByPassportNumber(string passportNumber);
    public Task<int> AddApplicantAsync(Applicant applicant);
    public Task<SearchModel<Applicant>> GetAllApplWithPredicateAsync(int pageNumber, int pageSize, string orderBy, SortingDirection sortingDirection, List<Expression<Func<Applicant, bool>>>? predicate = null, params string[] eagerLoadedProperties);
}