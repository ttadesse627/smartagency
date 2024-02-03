using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.SliderImageResponseDTOs;

namespace AppDiv.SmartAgency.Application.Features.PostImages
{
    public record GetAllPostImagesQuery : IRequest<ResponseContainerDTO<List<SliderImageResponseDTO>>>
    {

    }

    public class GetAllPostImagesHandler(IFileService fileService) : IRequestHandler<GetAllPostImagesQuery, ResponseContainerDTO<List<SliderImageResponseDTO>>>
    {
        private readonly IFileService _fileService = fileService;

        public Task<ResponseContainerDTO<List<SliderImageResponseDTO>>> Handle(GetAllPostImagesQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseContainerDTO<List<SliderImageResponseDTO>>();
            var sliderList = new List<SliderImageResponseDTO>();

            var imageList = _fileService.GetAllImages("SliderImages");
            if (imageList != null && imageList.Count != 0)
            {
                foreach (var image in imageList)
                {
                    var sliderImage = new SliderImageResponseDTO
                    {
                        FileContent = Convert.ToBase64String(image.Item1),
                        FileName = image.Item2,
                        FileExtension = image.Item3
                    };
                    sliderList.Add(sliderImage);
                }
                response.Items = sliderList;
            }

            return Task.FromResult(response);

        }
    }
}