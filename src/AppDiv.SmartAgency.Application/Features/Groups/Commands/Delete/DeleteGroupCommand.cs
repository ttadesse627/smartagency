
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Groups.Commands.Delete
{
    public class DeleteGroupCommand : IRequest<ServiceResponse<int>>
    {
        public Guid Id { get; set; }

    }
    public class DeleteGroupCommandsHandler : IRequestHandler<DeleteGroupCommand, ServiceResponse<int>>
    {
        private readonly IGroupRepository _groupRepository;
        public DeleteGroupCommandsHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<ServiceResponse<int>> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var response = new ServiceResponse<int>();
            try
            {
                var groupEntity = await _groupRepository.GetAsync(request.Id);

                await _groupRepository.DeleteAsync(request.Id);
                response.Success = await _groupRepository.SaveChangesAsync(cancellationToken);
                if (response.Success)
                {
                    response.Data = 1;
                    response.Message = "Group deletion succeeded.";
                }
            }
            catch (Exception exp)
            {
                response.Errors?.Add(exp.Message);
                throw (new ApplicationException(exp.Message));
            }
            return response;
        }
    }
}
