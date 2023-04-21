using AppDiv.SmartAgency.Application.Contracts.Request;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Features.Command.Create.Customers
{
    // Customer create command with CustomerResponse
    public record CreateCustomerCommand(AddCustomerRequest customer): IRequest<CreateCustomerCommandResponse>
    {
        
    }
    }