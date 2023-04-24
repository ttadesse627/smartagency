using AppDiv.SmartAgency.Application.Contracts.DTOs;

using AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;

using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;

using AppDiv.SmartAgency.Application.Features.Command.Create.Attachments;
using AppDiv.SmartAgency.Application.Features.Command.Create.Customers;
using AppDiv.SmartAgency.Application.Features.Command.Create.LookUps;
using AppDiv.SmartAgency.Application.Features.Command.Update.Attachments;
using AppDiv.SmartAgency.Application.Features.Customers.Command.Update;
using AppDiv.SmartAgency.Domain.Entities;
using AutoMapper;

namespace AppDiv.SmartAgency.Application.Mapper
{
    internal class SmartAgencyMappingProfile : Profile
    {
        public SmartAgencyMappingProfile()
        {

            CreateMap<Customer, CustomerResponseDTO>().ReverseMap();
            CreateMap<Customer, CreateCustomerCommand>().ReverseMap();
            CreateMap<Customer, EditCustomerCommand>().ReverseMap();

            CreateMap<Attachment, CreateAttachmentResponseDTO>().ReverseMap();
            CreateMap<Attachment, CreateAttachmentCommand>().ReverseMap();
            CreateMap<Attachment, AttachmentResponseDTO>().ReverseMap();

            CreateMap<LookUp, CreateLookUpResponseDTO>().ReverseMap();
            CreateMap<LookUp, CreateLookUpCommand>().ReverseMap();
            CreateMap<LookUp, LookUpResponseDTO>().ReverseMap();
            CreateMap<Attachment, EditAttachmentCommand>().ReverseMap();
            CreateMap<EditAttachmentCommand, AttachmentResponseDTO>().ReverseMap();
        }
    }
}
