using AppDiv.CRVS.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.CRVS.Application.Features.Customers.Command.Delete
{
    // Customer create command with string response
    public class DeleteCustomerCommand : IRequest<String>
    {
        public Guid Id { get; private set; }

        public DeleteCustomerCommand(Guid Id)
        {
            this.Id = Id;
        }
    }

    // Customer delete command handler with string response as output
    public class DeleteCustomerCommmandHandler : IRequestHandler<DeleteCustomerCommand, String>
    {
        private readonly ICustomerRepository _customerRepository;
        public DeleteCustomerCommmandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<string> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
               var customerEntity = await _customerRepository.GetByIdAsync(request.Id);

                await _customerRepository.DeleteAsync(customerEntity);
            }
            catch (Exception exp)
            {
                throw (new ApplicationException(exp.Message));
            }

            return "Customer information has been deleted!";
        }
    }
}