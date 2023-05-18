using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.Request.Partners;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Partners.Command.Create
{
    public record CreatePartnerCommand(CreatePartnerRequest partner) : IRequest<CreatePartnerCommandResponse>
{

}
}