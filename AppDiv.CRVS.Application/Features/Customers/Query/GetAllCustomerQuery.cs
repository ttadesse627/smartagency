using AppDiv.CRVS.Application.Contracts.DTOs;
using AppDiv.CRVS.Application.Mapper;
using AppDiv.CRVS.Domain.Entities;
using AppDiv.CRVS.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.CRVS.Application.Features.Customers.Query
{
    // Customer query with List<Customer> response
    public record GetAllCustomerQuery : IRequest<List<CustomerResponseDTO>>
    {

    }

    public class GetAllCustomerHandler : IRequestHandler<GetAllCustomerQuery, List<CustomerResponseDTO>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetAllCustomerHandler(ICustomerRepository customerQueryRepository)
        {
            _customerRepository = customerQueryRepository;
        }
        public async Task<List<CustomerResponseDTO>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
           var customerList= await _customerRepository.GetAllAsync();         
            var customerResponse = CustomMapper.Mapper.Map<List<CustomerResponseDTO>>(customerList);
            return customerResponse;

           // return (List<Customer>)await _customerQueryRepository.GetAllAsync();
        }
    }
}