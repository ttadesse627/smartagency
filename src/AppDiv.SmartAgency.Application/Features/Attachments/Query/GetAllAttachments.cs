using AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
using AppDiv.SmartAgency.Utility.Contracts;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Application.Features.Query.Attachments
{
    public class GetAllAttachments : IRequest<SearchModel<AttachmentResponseDTO>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchTerm { get; set; } = string.Empty;
        public string OrderBy { get; set; } = string.Empty;
        public SortingDirection SortingDirection { get; set; } = SortingDirection.Ascending;
        public GetAllAttachments(int pageNumber, int pageSize, string searchTerm, string orderBy, SortingDirection sortingDirection)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchTerm = searchTerm;
            OrderBy = orderBy;
            SortingDirection = sortingDirection;
        }

    }

    public class GetAllAttachmentsHandler : IRequestHandler<GetAllAttachments, SearchModel<AttachmentResponseDTO>>
    {
        private readonly IAttachmentRepository _attachmentRepository;

        public GetAllAttachmentsHandler(IAttachmentRepository attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
        }
        public async Task<SearchModel<AttachmentResponseDTO>> Handle(GetAllAttachments request, CancellationToken cancellationToken)
        {
            var attachmentList = await _attachmentRepository.GetAllWithSearchAsync(request.PageNumber, request.PageSize, request.SearchTerm, request.OrderBy, request.SortingDirection);
            var attachmentResponse = CustomMapper.Mapper.Map<SearchModel<AttachmentResponseDTO>>(attachmentList);
            return attachmentResponse;
        }
    }
}