using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.Request.Partners;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Command.Create.Partners
{
    public record CreatePartnerCommand(CreatePartnerRequest partner) : IRequest<CreatePartnerCommandResponse>
{

}
}