using AppDiv.CRVS.Application.Contracts.DTOs;
using AppDiv.CRVS.Application.Mapper;
using AppDiv.CRVS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.CRVS.Application.Features.Customers.Query
{
    // Customer GetCustomerByEmailQuery with Customer response
    public class GetCustomerByEmailQuery : IRequest<Customer>
    {
        public string Email { get; private set; }

        public GetCustomerByEmailQuery(string email)
        {
            this.Email = email;
        }

    }

    public class GetCustomerByEmailHandler : IRequestHandler<GetCustomerByEmailQuery, Customer>
    {
        private readonly IMediator _mediator;

        public GetCustomerByEmailHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Customer> Handle(GetCustomerByEmailQuery request, CancellationToken cancellationToken)
        {
            var customers = await _mediator.Send(new GetAllCustomerQuery());
            var selectedCustomer = customers.FirstOrDefault(x => x.Email.ToLower().Contains(request.Email.ToLower()));

            return CustomMapper.Mapper.Map<Customer>(selectedCustomer); 
        }
    }
}