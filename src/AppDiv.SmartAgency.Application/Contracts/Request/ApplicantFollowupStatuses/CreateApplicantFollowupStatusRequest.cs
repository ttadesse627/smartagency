using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Contracts.Request.ApplicantFollowupStatuses
{
    public class CreateApplicantFollowupStatusRequest
    {

        public string? PassportNumber { get; set; }
        public DateTime Month { get; set; }
        public string? Remark { get; set; }
        public Guid FollowupStatusId { get; set; }

    }
}