using AppDiv.CRVS.Application.Contracts.Request;
using AppDiv.CRVS.Application.Mapper;
using AppDiv.CRVS.Domain.Entities;
using AppDiv.CRVS.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.CRVS.Application.Features.Customers.Command.Create
{
    // Customer create command with CustomerResponse
    public record CreateCustomerCommand(AddCustomerRequest customer): IRequest<CreateCustomerCommandResponse>
    {
       
    }
    }