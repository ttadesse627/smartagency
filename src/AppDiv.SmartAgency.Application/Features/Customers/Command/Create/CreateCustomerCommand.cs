﻿using AppDiv.SmartAgency.Application.Contracts.Request;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Features.Customers.Command.Create
{
    // Customer create command with CustomerResponse
    public record CreateCustomerCommand(AddCustomerRequest customer): IRequest<CreateCustomerCommandResponse>
    {
        
    }
    }