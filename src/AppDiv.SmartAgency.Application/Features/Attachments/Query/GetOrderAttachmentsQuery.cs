

using global::AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
using global::AppDiv.SmartAgency.Application.Interfaces.Persistence;
using global::AppDiv.SmartAgency.Application.Interfaces.Persistence.Base;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Attachments.Query
{
    public record GetOrderAttachmentsQuery : IRequest<List<AttachmentDropDownDto>> { }
    public class GetOrderAttachmentsQueryHandler : IRequestHandler<GetOrderAttachmentsQuery, List<AttachmentDropDownDto>>
    {
        private readonly IAttachmentRepository _attachmentRepository;


        public GetOrderAttachmentsQueryHandler(IAttachmentRepository attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;

        }
        public async Task<List<AttachmentDropDownDto>> Handle(GetOrderAttachmentsQuery request, CancellationToken cancellationToken)
        {


            return await _attachmentRepository.GetOrderAttachments();

            // var attchdropDowns = await _attachmentRepository.GetAllWithAsync();
            // var attachment = new AttachmntResponseDTO();
            // var dropDownResponse = new List<AttachmentDropDownDto>();
            // foreach (var attItem in attchdropDowns)
            // {
            //     dropDownResponse.Add(new AttachmentDropDownDto { Key = attItem.Id, Value = attItem.Title });
            // }

            // attachment.Attachments = dropDownResponse;
            // return attachment;





        }
    }
}
