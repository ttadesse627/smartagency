using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Interfaces.Persistence
{
    public interface IApplicantFollowupStatusRepository: IBaseRepository<ApplicantFollowupStatus>
    {
        Task<ApplicantFollowupStatus> GetByIdAsync(Guid id);
       // Task<Int32> UpdateAsync(ApplicantFollowupStatus applicantFollowupStatus);
    }
}
