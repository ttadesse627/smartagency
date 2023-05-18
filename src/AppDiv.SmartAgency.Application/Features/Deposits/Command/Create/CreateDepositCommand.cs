using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.Request.Deposits;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Command.Create.Deposits
{
   public record CreateDepositCommand(CreateDepositRequest deposit) : IRequest<CreateDepositCommandResponse>
{

}
}