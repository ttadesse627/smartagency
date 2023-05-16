using AppDiv.SmartAgency.Application.Contracts.DTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.CategoryDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.DepositDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants;
using AppDiv.SmartAgency.Application.Contracts.Request.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Orders;

using AppDiv.SmartAgency.Application.Contracts.Request.Deposits;
using AppDiv.SmartAgency.Application.Contracts.Request.Partners;
using AppDiv.SmartAgency.Application.Features.Command.Create.Attachments;
using AppDiv.SmartAgency.Application.Features.Command.Create.Customers;
using AppDiv.SmartAgency.Application.Features.Command.Create.LookUps;
using AppDiv.SmartAgency.Application.Features.Command.Update.Attachments;
using AppDiv.SmartAgency.Application.Features.Command.Update.Deposits;
using AppDiv.SmartAgency.Application.Features.Command.Update.Partners;
using AppDiv.SmartAgency.Application.Features.Customers.Command.Update;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using AutoMapper;
using AppDiv.SmartAgency.Application.Common;
using System.Collections;
using AppDiv.SmartAgency.Utility.Contracts;
using AppDiv.SmartAgency.Application.Contracts.Request.Pagess;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PageDTOs;
using AppDiv.SmartAgency.Application.Features.Command.Update.Pagess;

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

            CreateMap<Category, CategoryResponseDTO>();
            CreateMap<LookUp, CreateLookUpResponseDTO>().ReverseMap();
            CreateMap<LookUp, CreateLookUpCommand>().ReverseMap();
            CreateMap<LookUp, LookUpResponseDTO>().ReverseMap();

            CreateMap<CreateApplicantRequest, Applicant>().ForMember(dest => dest.ApplicantTechnicalSkills, opt => opt.Ignore());
            CreateMap<LanguageRequest, Language>();
            CreateMap<ExperienceRequest, Experience>();
            CreateMap<EducationRequest, Education>().ForMember(dest => dest.EducationLevelofQualifications, opt => opt.Ignore())
                                                    .ForMember(dest => dest.EducationQualificationTypes, opt => opt.Ignore())
                                                    .ForMember(dest => dest.EducationAwards, opt => opt.Ignore());
            CreateMap<BankAccountRequest, BankAccount>();
            CreateMap<BeneficiaryRequest, Beneficiary>();
            CreateMap<EmergencyContactRequest, EmergencyContact>();
            CreateMap<RepersentativeRequest, Repersentative>();
            CreateMap<WitnessRequest, Witness>();
            CreateMap<AddressRequest, Address>();
            CreateMap<FileCollectionRequest, FileCollection>();

            CreateMap<Applicant, CreateApplicantResponseDTO>();
            CreateMap<Category, CategoryResponseDTO>();
            CreateMap<Applicant, ApplicantsResponseDTO>();
            CreateMap<BankAccount, BankAccountResponseDTO>();
            CreateMap<Beneficiary, BeneficiaryResponseDTO>();
            CreateMap<Education, EducationResponseDTO>();
            CreateMap<EmergencyContact, EmergencyContactResponseDTO>();
            CreateMap<FileCollection, FileCollectionResponseDTO>();
            CreateMap<Language, LanguageResponseDTO>();
            CreateMap<Partner, PartnerApplRespDTO>();
            CreateMap<Repersentative, RepresentativeResponseDTO>();
            CreateMap<Witness, WitnessResponseDTO>();
            CreateMap<Applicant, ApplicantsResponseDTO>();

            CreateMap<CreatePartnerRequest, Partner>();
            CreateMap<Partner, PartnerResponseDTO>();
            CreateMap<Address, AddressResponseDTO>();

            CreateMap<CreateOrderRequest, Order>();
            CreateMap<OrderCriteriaRequest, OrderCriteria>();
            CreateMap<SponsorRequest, Sponsor>();
            CreateMap<PaymentRequest, Payment>();

            CreateMap<Order, OrderResponseDTO>();
            CreateMap<OrderCriteria, OrderCriteriaResponseDTO>();
            CreateMap<Payment, PaymentResponseDTO>();
            CreateMap<Sponsor, SponsorResponseDTO>();
            CreateMap<Applicant, OrderApplResponseDTO>();
            CreateMap<EditOrderRequest, Order>();
            CreateMap<EditOrderCriteriaRequest, OrderCriteria>();
            CreateMap<EditSponsorRequest, Sponsor>();
            CreateMap<EditFileCollectionRequest, FileCollection>();
            CreateMap<EditPaymentRequest, Payment>();
            CreateMap<EditAddressRequest, Address>();

            CreateMap<Partner, PartnerResponseDTO>();
            CreateMap<Address, AddressResponseDTO>();
            CreateMap<EditPartnerCommand, Partner>();

            CreateMap<OnlineApplicant, OnlineApplicantResponseDTO>();
            CreateMap<OnlineApplicantRequest, OnlineApplicant>();
            CreateMap<LookUp, OnlineApplicantLookUpResponseDTO>();

            CreateMap<Deposit, DepositResponseDTO>();
            CreateMap<CreateDepositRequest, Deposit>();
            CreateMap<Applicant, DepositApplicantResponseDTO>();
            CreateMap<DepositResponseDTO, GetDepositByIdResponseDTO>();
            CreateMap<EditDepositCommand, Deposit>();
            CreateMap<Deposit, GetDepositByIdResponseDTO>();

            CreateMap<ApplicantFollowupStatus, ApplicantFollowupStatusResponseDTO>();
            CreateMap<CreateApplicantFollowupStatusRequest, ApplicantFollowupStatus>();

            CreateMap<SearchModel<LookUp>, SearchModel<LookUpResponseDTO>>();
            CreateMap<SearchModel<Attachment>, SearchModel<AttachmentResponseDTO>>();

            CreateMap<CreatePageRequest, Page>();
            CreateMap<Page, PageResponseDTO>();
            CreateMap<EditPageCommand, Page>();

        }
    }
}
