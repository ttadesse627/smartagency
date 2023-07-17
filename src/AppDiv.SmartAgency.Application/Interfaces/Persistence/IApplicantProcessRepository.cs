

using AppDiv.SmartAgency.Application.Contracts.DTOs.DashbourdDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.QuickLinksDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities;
using Newtonsoft.Json.Linq;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence;
public interface IApplicantProcessRepository : IBaseRepository<ApplicantProcess>
{

    public Task<object> GetDashbourdResult(DateTime? startDate, DateTime? endDate);
    public Task<List<JObject>> GetQuickLinks();
    public Task<List<NavBarResponseDTO>> GetNavBars(DateTime? startDate, DateTime? endDate);
}