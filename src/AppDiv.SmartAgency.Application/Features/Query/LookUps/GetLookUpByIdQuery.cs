using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Utility.Contracts;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Query.LookUps
{
    public record GetLookUpByIdQuery: IRequest<LookUp>
    {
        public Guid Id { get; private set; }

        public GetLookUpByIdQuery(Guid Id)
        {
            this.Id = Id;
        }
    }
}