using AppDiv.SmartAgency.Application.Contracts.DTOs;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Features.Customers.Command.Update
{
    // Customer create command with CustomerResponse
    public class EditCustomerCommand : IRequest<CustomerResponseDTO>
    {

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
    }

    public class EditCustomerCommandHandler : IRequestHandler<EditCustomerCommand, CustomerResponseDTO>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerRepository _customerQueryRepository;
        public EditCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<CustomerResponseDTO> Handle(EditCustomerCommand request, CancellationToken cancellationToken)
        {
           // var customerEntity = CustomerMapper.Mapper.Map<Customer>(request);
            Customer customerEntity = new Customer
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
            };

            try
            {
                await _customerRepository.UpdateAsync(customerEntity,x=>x.Id);
            }
            catch (Exception exp)
            {
                throw new ApplicationException(exp.Message);
            }

            var modifiedCustomer = await _customerQueryRepository.GetByIdAsync(request.Id);
            var customerResponse = CustomMapper.Mapper.Map<CustomerResponseDTO>(modifiedCustomer);

            return customerResponse;
        }
    }
}