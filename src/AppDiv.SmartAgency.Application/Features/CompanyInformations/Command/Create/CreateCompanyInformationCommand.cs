using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.Request.CompanyInformations;
using AppDiv.SmartAgency.Application.Contracts.Request.Partners;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.CompanyInformations.Command.Create
{
    public record CreateCompanyInformationCommand(CreateCompanyInformationRequest companyInformation) : IRequest<CreateCompanyInformationCommandResponse>
{

}
}