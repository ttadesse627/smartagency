
using AppDiv.SmartAgency.Application.Contracts.DTOs.GroupDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Groups.Query.GetGroupById
{
    public class GetGroupbyId(Guid Id) : IRequest<GroupDTO>
    {
        public Guid Id { get; private set; } = Id;
    }

    public class GetGroupbyIdHandler : IRequestHandler<GetGroupbyId, GroupDTO>
    {
        private readonly IGroupRepository _groupRepository;

        public GetGroupbyIdHandler(IGroupRepository groupRepository)
        {

            _groupRepository = groupRepository;
        }
        public async Task<GroupDTO> Handle(GetGroupbyId request, CancellationToken cancellationToken)
        {
            var selectedGroup = await _groupRepository.GetAsync(request.Id);
            return CustomMapper.Mapper.Map<GroupDTO>(selectedGroup);
        }
    }
}