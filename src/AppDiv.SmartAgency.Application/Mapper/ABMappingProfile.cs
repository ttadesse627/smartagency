using AppDiv.SmartAgency.Application.Contracts.DTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;

using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
using AppDiv.SmartAgency.Application.Contracts.Request.Common;
using AppDiv.SmartAgency.Application.Features.Command.Create.Attachments;
using AppDiv.SmartAgency.Application.Features.Command.Create.Customers;
using AppDiv.SmartAgency.Application.Features.Command.Create.LookUps;
using AppDiv.SmartAgency.Application.Features.Command.Update.Attachments;
using AppDiv.SmartAgency.Application.Features.Customers.Command.Update;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;
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
            CreateMap<Attachment, EditAttachmentCommand>().ReverseMap();
            CreateMap<EditAttachmentCommand, AttachmentResponseDTO>().ReverseMap();

            CreateMap<LookUp, CreateLookUpResponseDTO>().ReverseMap();
            CreateMap<LookUp, CreateLookUpCommand>().ReverseMap();
            CreateMap<LookUp, LookUpResponseDTO>().ReverseMap();

            CreateMap<Applicant, CreateApplicantRequest>().ReverseMap();
            CreateMap<Applicant, CreateApplicantResponseDTO>().ReverseMap();
            CreateMap<Language, LanguageRequest>().ReverseMap();
            CreateMap<TechnicalSkill, TechnicalSkillRequest>().ReverseMap();
            CreateMap<Experience, ExperienceRequest>().ReverseMap();
            CreateMap<Education, EducationRequest>().ReverseMap();
            CreateMap<BankAccount, BankAccountRequest>().ReverseMap();
            CreateMap<Applicant, CreateApplicantRequest>().ReverseMap();
            // .ForMember(dest => dest.Beneficiaries, opt => opt.MapFrom(src => src.Beneficiaries));
            CreateMap<CreateApplicantRequest, Applicant>().ReverseMap();
            // .ForMember(dest => dest.Beneficiaries, opt => opt.MapFrom(src => src.Beneficiaries));
            CreateMap<Beneficiary, BeneficiaryRequest>().ReverseMap();
            // .ForMember(dest => dest.Relationship, opt => opt.MapFrom(src => src.Relationship));
            CreateMap<EmergencyContact, EmergencyContactRequest>().ReverseMap();
            CreateMap<Repersentative, RepersentativeRequest>().ReverseMap();
            CreateMap<Witness, WitnessRequest>().ReverseMap();
            CreateMap<Address, AddressRequest>().ReverseMap();
        }
    }
}
