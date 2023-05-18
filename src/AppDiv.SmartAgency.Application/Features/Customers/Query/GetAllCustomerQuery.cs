using AppDiv.SmartAgency.Application.Contracts.DTOs;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Features.Query.Customers
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
            var customerList = await _customerRepository.GetAllAsync();
            var customerResponse = CustomMapper.Mapper.Map<List<CustomerResponseDTO>>(customerList);
            return customerResponse;

            // return (List<Customer>)await _customerQueryRepository.GetAllAsync();
        }
    }
}