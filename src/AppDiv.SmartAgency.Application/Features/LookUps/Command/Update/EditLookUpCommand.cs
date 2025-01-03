using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Utility.Exceptions;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.LookUps.Command.Update
{
    public record EditLookUpCommand : IRequest<ServiceResponse<Int32>>
    {
        public Guid Id { get; set; }
        public string? Category { get; set; }
        public string? Value { get; set; }
    }


    public class EditLookUpCommandHandler(ILookUpRepository lookUpRepository) : IRequestHandler<EditLookUpCommand, ServiceResponse<Int32>>
    {
        private readonly ILookUpRepository _lookUpRepository = lookUpRepository;

        public async Task<ServiceResponse<int>> Handle(EditLookUpCommand request, CancellationToken cancellationToken)
        {
            var response = new ServiceResponse<int>();
            var lookUpEntity = await _lookUpRepository.GetAsync(request.Id);

            if (lookUpEntity != null)
            {
                CustomMapper.Mapper.Map(request, lookUpEntity);
                try
                {
                    response.Success = await _lookUpRepository.SaveChangesAsync(cancellationToken);
                    if (response.Success)
                    {
                        response.Message = "Update Successful!";
                    }
                }
                catch (Exception exp)
                {
                    response.Errors?.Add(exp.Message);
                    throw new ApplicationException(exp.Message);
                }
            }
            else
            {
                response.Errors?.Add(new NotFoundException($"Lookup not found!").Message);
                throw new NotFoundException($"Lookup not found!");
            }
            return response;
        }
    }

}