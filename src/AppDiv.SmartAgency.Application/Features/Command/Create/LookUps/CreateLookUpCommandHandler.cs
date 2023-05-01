using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;





namespace AppDiv.SmartAgency.Application.Features.Command.Create.LookUps
{

    public class CreateLookUpCommandHandler : IRequestHandler<CreateLookUpCommand, CreateLookUpCommandResponse>
    {
        private readonly ILookUpRepository _lookUpRepository;
        private readonly ICategoryRepository _categoryRepository;
        public CreateLookUpCommandHandler(ILookUpRepository lookUpRepository, ICategoryRepository categoryRepository)
        {
            _lookUpRepository = lookUpRepository;
            _categoryRepository = categoryRepository;
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
                var lookUp = new LookUp()
                {
                    CategoryId = request.lookUp.CategoryId,
                    Value = request.lookUp.Value
                };

                await _lookUpRepository.InsertAsync(lookUp, cancellationToken);
                createLookUpCommandResponse.Success = await _lookUpRepository.SaveChangesAsync(cancellationToken);
                IEnumerable<LookUp> lookUps = await _lookUpRepository.GetAllAsync();
                Console.WriteLine(lookUp);

                if (lookUp.CategoryId.ToString() == "ca546868-3cd5-4038-a1df-dbf34509407f")
                {
                    var category = new Category { Name = lookUp.Value };
                    await _categoryRepository.InsertAsync(category, cancellationToken);
                    await _categoryRepository.SaveChangesAsync(cancellationToken);
                }

                if (createLookUpCommandResponse.Success)
                {
                    createLookUpCommandResponse.Message = "Successfully inserted!";
                }
                else createLookUpCommandResponse.Message = "Failed to save your data!";
            }
            return createLookUpCommandResponse;
        }
    }
}