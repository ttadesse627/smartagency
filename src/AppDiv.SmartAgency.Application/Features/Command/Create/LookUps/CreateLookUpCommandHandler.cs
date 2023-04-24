using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;





namespace AppDiv.SmartAgency.Application.Features.Command.Create.LookUps
{
    
public class CreateLookUpCommandHandler : IRequestHandler<CreateLookUpCommand, CreateLookUpCommandResponse>
{
        private readonly ILookUpRepository _lookUpRepository;
        public CreateLookUpCommandHandler(ILookUpRepository lookUpRepository)
        {
            _lookUpRepository = lookUpRepository;
        }
        public async Task<CreateLookUpCommandResponse> Handle(CreateLookUpCommand request, CancellationToken cancellationToken)
        {
           // var customerEntity = CustomerMapper.Mapper.Map<Customer>(request.customer);           

            var createLookUpCommandResponse = new CreateLookUpCommandResponse();

            var validator = new CreateLookUpCommandValidator(_lookUpRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            //Check and log validation errors
            if (validationResult.Errors.Count > 0)
            {
                createLookUpCommandResponse.Success = false;
                createLookUpCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                    createLookUpCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                createLookUpCommandResponse.Message = createLookUpCommandResponse.ValidationErrors[0];
            }
            if (createLookUpCommandResponse.Success)
            {
                //can use this instead of automapper
                var lookUp = new LookUp()
                {
                    CategoryId=request.lookUp.CategoryId,
                    Value=request.lookUp.Value
                    
                };
                //
                await _lookUpRepository.InsertAsync(lookUp, cancellationToken);
                await _lookUpRepository.SaveChangesAsync(cancellationToken);

                //var customerResponse = CustomerMapper.Mapper.Map<CustomerResponseDTO>(customer);
               // createCustomerCommandResponse.Customer = customerResponse;          
            }
            return createLookUpCommandResponse;
        }
}
}