using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.LookUps.Command.Update
{
    public record EditLookUpCommand : IRequest<ServiceResponse<Int32>>
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string Value { get; set; }
    }


    public class EditLookUpCommandHandler : IRequestHandler<EditLookUpCommand, ServiceResponse<Int32>>
    {
        private readonly ILookUpRepository _lookUpRepository;
        public EditLookUpCommandHandler(ILookUpRepository lookUpRepository)
        {
            _lookUpRepository = lookUpRepository;
        }
        public async Task<ServiceResponse<Int32>> Handle(EditLookUpCommand request, CancellationToken cancellationToken)
        {
            var response = new ServiceResponse<Int32>();
            var lookUpEntity = await _lookUpRepository.GetAsync(request.Id);

            if (lookUpEntity != null)
            {
                var edited = CustomMapper.Mapper.Map(request, lookUpEntity);
                lookUpEntity = edited;
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
                    throw new System.ApplicationException(exp.Message);
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