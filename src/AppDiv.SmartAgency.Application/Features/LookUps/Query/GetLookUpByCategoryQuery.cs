using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.LookUps.Query
{
    public record GetLookUpByCategoryQuery(List<string> categories) : IRequest<Dictionary<string, List<LookUpItemResponseDTO>>> { }
    public class GetLookUpByCategoryQueryHandler : IRequestHandler<GetLookUpByCategoryQuery, Dictionary<string, List<LookUpItemResponseDTO>>>
    {
        private readonly ILookUpRepository _lookUpRepository;

        public GetLookUpByCategoryQueryHandler(ILookUpRepository lookUpRepository)
        {
            _lookUpRepository = lookUpRepository;
        }
        public async Task<Dictionary<string, List<LookUpItemResponseDTO>>> Handle(GetLookUpByCategoryQuery request, CancellationToken cancellationToken)
        {
            var lookUps = await _lookUpRepository.GetAllKeysAsync(request.categories);

            // Group the lookups by category and create a dictionary of category names and their corresponding items
            var groupedItems = lookUps.GroupBy(l => l.Category)
                .ToDictionary(group => group.Key, group => group.Select(item => new LookUpItemResponseDTO
                {
                    Id = item.Id,
                    Value = item.Value
                }).ToList());

            return groupedItems;
        }
    }
}