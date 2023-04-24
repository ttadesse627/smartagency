using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.LookUps
{
    public class GetLookUpByIdQuery: IRequest<LookUp>
    {
        public string Id { get; private set; }

        public GetLookUpByIdQuery(string Id)
        {
            this.Id = Id;
        }
    }
}