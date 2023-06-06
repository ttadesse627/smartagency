using AppDiv.SmartAgency.Application.Contracts.DTOs;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*namespace AppDiv.SmartAgency.Application.Features.PostImages
{
    // Customer query with List<Customer> response
    public record GetAllPostImagesQuery : IRequest<string>
    {

    }

    public class GetAllPostImagesHandler : IRequestHandler<GetAllPostImagesQuery, string>
    {
        private readonly IFileService _fileService;

        public GetAllPostImagesHandler(IFileService fileService)
        {
            _fileService = fileService;
        }
        public async Task<string> Handle(GetAllPostImagesQuery request, CancellationToken cancellationToken)
        {

            var imageList = await _customerRepository.GetAllAsync();
            var customerResponse = CustomMapper.Mapper.Map<List<CustomerResponseDTO>>(customerList);
            return customerResponse;

            // return (List<Customer>)await _customerQueryRepository.GetAllAsync();
        }
    }
}

*/