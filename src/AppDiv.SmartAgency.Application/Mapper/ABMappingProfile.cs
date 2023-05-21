using AppDiv.SmartAgency.Application.Contracts.DTOs;
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
// using AppDiv.SmartAgency.Application.Features.Command.Create.Attachments;
// using AppDiv.SmartAgency.Application.Features.Command.Create.Customers;
// using AppDiv.SmartAgency.Application.Features.Command.Update.Attachments;
// using AppDiv.SmartAgency.Application.Features.Command.Update.Deposits;
// using AppDiv.SmartAgency.Application.Features.Command.Update.Partners;
using AppDiv.SmartAgency.Application.Features.Customers.Command.Update;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Entities.Orders;
using AutoMapper;
using AppDiv.SmartAgency.Utility.Contracts;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OnlineApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.OnlineApplicants;
using AppDiv.SmartAgency.Application.Contracts.Request.ApplicantFollowupStatuses;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PageDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Pagess;
using AppDiv.SmartAgency.Application.Features.Pages.Command.Update;
using AppDiv.SmartAgency.Application.Features.LookUps.Command.Create;
using AppDiv.SmartAgency.Application.Features.Customers.Command.Create;
using AppDiv.SmartAgency.Application.Features.Attachments.Command.Create;
using AppDiv.SmartAgency.Application.Features.Attachments.Command.Update;
using AppDiv.SmartAgency.Application.Features.Partners.Command.Update;
using AppDiv.SmartAgency.Application.Features.Deposits.Command.Update;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantFollowupStatusDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantFollowupStatusResponseDTOs;

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

            CreateMap<CreateApplicantRequest, Applicant>()
                .ForMember(dest => dest.Skills, opt => opt.Ignore());
            CreateMap<LanguageSkillRequest, LanguageSkill>();
            CreateMap<ExperienceRequest, Experience>();
            CreateMap<EducationRequest, Education>()
                .ForMember(dest => dest.QualificationTypes, opt => opt.Ignore())
                .ForMember(dest => dest.LevelofQualifications, opt => opt.Ignore())
                .ForMember(dest => dest.Awards, opt => opt.Ignore());
            CreateMap<BankAccountRequest, BankAccount>();
            CreateMap<EmergencyContactRequest, EmergencyContact>();
            CreateMap<RepresentativeRequest, Representative>();
            CreateMap<WitnessRequest, Witness>();
            CreateMap<BeneficiaryRequest, Beneficiary>();
            CreateMap<AttachmentFileRequest, AttachmentFile>();
            CreateMap<AddressRequest, Address>();
            CreateMap<RepAddressRequest, Address>();

            CreateMap<Applicant, CreateApplicantResponseDTO>();
            CreateMap<SearchModel<Applicant>, SearchModel<ApplicantsResponseDTO>>();
            CreateMap<BankAccount, BankAccountResponseDTO>();
            CreateMap<Beneficiary, BeneficiaryResponseDTO>();
            CreateMap<Education, EducationResponseDTO>();
            CreateMap<EmergencyContact, EmergencyContactResponseDTO>();
            CreateMap<AttachmentFile, FileCollectionResponseDTO>();
            CreateMap<LanguageSkill, LanguageResponseDTO>();
            CreateMap<Partner, PartnerApplRespDTO>();
            CreateMap<Representative, RepresentativeResponseDTO>();
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
            CreateMap<EditPaymentRequest, Payment>();
            CreateMap<EditAddressRequest, Address>();

            
            CreateMap<Address, AddressResponseDTO>();
          
           CreateMap<CreatePartnerRequest, Partner>();
            CreateMap<Address, AddressResponseDTO>();

            CreateMap<Partner, PartnerResponseDTO>();
            CreateMap<Partner, GetAllPartnerResponseDTO>();
            CreateMap<Address, PartnerAddressResponseDTO>();
            CreateMap<Address, GetAllPartnerAddressResponseDTO>();
            CreateMap<EditPartnerCommand, Partner>();
            CreateMap<PartnerAddressRequest, Address>();
            CreateMap<EditPartnerAddressRequest, Address>();
            CreateMap<SearchModel<Partner>, SearchModel<GetAllPartnerResponseDTO>>();


            CreateMap<OnlineApplicant, OnlineApplicantResponseDTO>();
            CreateMap<OnlineApplicantRequest, OnlineApplicant>();
            CreateMap<LookUp, OnlineApplicantLookUpResponseDTO>();

            CreateMap<Deposit, DepositResponseDTO>();
            CreateMap<CreateDepositRequest, Deposit>();
            CreateMap<Applicant, DepositApplicantResponseDTO>();
            CreateMap<DepositResponseDTO, GetDepositByIdResponseDTO>();
            CreateMap<EditDepositCommand, Deposit>();
            CreateMap<Deposit, GetDepositByIdResponseDTO>();
            CreateMap<SearchModel<Deposit>, SearchModel<DepositResponseDTO>>();

            CreateMap<SearchModel<ApplicantFollowupStatus>, SearchModel<ApplicantFollowupStatusResponseDTO>>();
            CreateMap<ApplicantFollowupStatus, ApplicantFollowupStatusResponseDTO>();
            CreateMap<CreateApplicantFollowupStatusRequest, ApplicantFollowupStatus>();
            CreateMap<ApplicantFollowupStatus, GetApplicantFollowupStatusByIdResponseDTO>();

            
            CreateMap<SearchModel<LookUp>, SearchModel<LookUpResponseDTO>>();

            CreateMap<SearchModel<Attachment>, SearchModel<AttachmentResponseDTO>>();

            
            CreateMap<SearchModel<OnlineApplicant>, SearchModel<OnlineApplicantResponseDTO>>();

            CreateMap<SearchModel<Page>, SearchModel<PageResponseDTO>>();
            CreateMap<CreatePageRequest, Page>();
            CreateMap<Page, PageResponseDTO>();
            CreateMap<EditPageCommand, Page>();
            CreateMap<Page, GetPageByIdResponseDTO>();

        }
    }
}
