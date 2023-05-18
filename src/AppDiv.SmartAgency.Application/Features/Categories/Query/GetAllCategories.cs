using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;
using AppDiv.SmartAgency.Application.Contracts.DTOs.CategoryDTOs;

namespace AppDiv.SmartAgency.Application.Features.Categories.Query;
public class GetAllCategories : IRequest<List<CategoryResponseDTO>>
{
}

public class GetAllCategorysHandler : IRequestHandler<GetAllCategories, List<CategoryResponseDTO>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetAllCategorysHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<CategoryResponseDTO>> Handle(GetAllCategories request, CancellationToken cancellationToken)
    {
        var categoriesList = await _categoryRepository.GetAllAsync();
        var categoryResponse = CustomMapper.Mapper.Map<List<CategoryResponseDTO>>(categoriesList);
        return categoryResponse;
    }
}