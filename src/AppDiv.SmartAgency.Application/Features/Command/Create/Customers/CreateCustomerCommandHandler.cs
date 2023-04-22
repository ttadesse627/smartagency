using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Contracts.DTOs;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;
using ApplicationException = AppDiv.SmartAgency.Application.Exceptions.ApplicationException;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;

namespace AppDiv.SmartAgency.Application.Features.Command.Create.Customers
{

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerCommandResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<CreateCustomerCommandResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
           // var customerEntity = CustomerMapper.Mapper.Map<Customer>(request.customer);           

            var createCustomerCommandResponse = new CreateCustomerCommandResponse();

            var validator = new CreateCustomerCommandValidator(_customerRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //Check and log validation errors
            if (validationResult.Errors.Count > 0)
            {
                createCustomerCommandResponse.Success = false;
                createCustomerCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                    createCustomerCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                createCustomerCommandResponse.Message = createCustomerCommandResponse.ValidationErrors[0];
            }
            if (createCustomerCommandResponse.Success)
            {
                //can use this instead of automapper
                var customer = new Customer()
                {
                    Id=Guid.NewGuid(),
                    FirstName=request.customer.FirstName,
                    Address=request.customer.Address,
                    LastName=request.customer.LastName
                };
                //
                await _customerRepository.InsertAsync(customer, cancellationToken);
                var result = await _customerRepository.SaveChangesAsync(cancellationToken);

                //var customerResponse = CustomerMapper.Mapper.Map<CustomerResponseDTO>(customer);
               // createCustomerCommandResponse.Customer = customerResponse;          
            }
            return createCustomerCommandResponse;
        }
    }
}
