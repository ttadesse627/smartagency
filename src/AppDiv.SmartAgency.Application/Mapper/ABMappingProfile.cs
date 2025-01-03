﻿using AppDiv.SmartAgency.Application.Contracts.DTOs.AttachmentDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.CategoryDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.DepositDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.LookUpDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrdersDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants.CreateApplicantRequests;
using AppDiv.SmartAgency.Application.Contracts.Request.Common;
using AppDiv.SmartAgency.Application.Contracts.Request.Orders;
using AppDiv.SmartAgency.Application.Contracts.Request.Enjazs;

using AppDiv.SmartAgency.Application.Contracts.Request.Deposits;
using AppDiv.SmartAgency.Application.Contracts.Request.Partners;
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
using AppDiv.SmartAgency.Application.Features.Attachments.Command.Create;
using AppDiv.SmartAgency.Application.Features.Attachments.Command.Update;
using AppDiv.SmartAgency.Application.Features.Partners.Command.Update;
using AppDiv.SmartAgency.Application.Features.Deposits.Command.Update;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.Applicants.EditApplicantRequests;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantFollowupStatusDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantDTOs.GetSingleApplResponseDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.GetOrderDTOs;
using AppDiv.SmartAgency.Application.Common;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ApplicantFollowupStatusResponseDTOs;
using AppDiv.SmartAgency.Application.Features.ApplicantsFollowupStatuses.Command.Update;
using AppDiv.SmartAgency.Application.Contracts.Request.CompanyInformations;
using AppDiv.SmartAgency.Application.Contracts.DTOs.CompanyInformationDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.OrderDTOs.OrderAssignment;
using AppDiv.SmartAgency.Application.Contracts.DTOs.ProcessDTOs;
using AppDiv.SmartAgency.Application.Contracts.Request.ProcessRequests;
using AppDiv.SmartAgency.Application.Features.LookUps.Command.Update;
using AppDiv.SmartAgency.Application.Contracts.Request.UserRequests;
using AppDiv.SmartAgency.Application.Contracts.DTOs.UserDTOs;
using AppDiv.SmartAgency.Application.Contracts.DTOs.GroupDTOs;
using AppDiv.SmartAgency.Application.Features.CompanyInformations.Command.Update;
using AppDiv.SmartAgency.Application.Contracts.Request.Groups;

namespace AppDiv.SmartAgency.Application.Mapper
{
    internal class SmartAgencyMappingProfile : Profile
    {
        public SmartAgencyMappingProfile()
        {
            CreateMap<Attachment, CreateAttachmentResponseDTO>().ReverseMap();
            CreateMap<Attachment, CreateAttachmentCommand>().ReverseMap();
            CreateMap<Attachment, AttachmentResponseDTO>().ReverseMap();
            CreateMap<Attachment, EditAttachmentCommand>().ReverseMap();
            CreateMap<EditAttachmentCommand, AttachmentResponseDTO>().ReverseMap();

            CreateMap<Category, CategoryResponseDTO>();
            CreateMap<LookUp, CreateLookUpResponseDTO>().ReverseMap();
            CreateMap<LookUp, CreateLookUpCommand>().ReverseMap();
            CreateMap<LookUp, LookUpResponseDTO>().ReverseMap();
            CreateMap<LookUp, LookUpItemResponseDTO>().ReverseMap();

            CreateMap<CreateApplicantRequest, Applicant>()
                .ForMember(dest => dest.Skills, opt => opt.Ignore())
                .ForMember(dest => dest.Witnesses, opt => opt.Ignore())
                .ForMember(dest => dest.Representative, opt => opt.Ignore())
                .ForMember(dest => dest.Experiences, opt => opt.Ignore())
                .ForMember(dest => dest.Beneficiaries, opt => opt.Ignore())
                .ForMember(dest => dest.LanguageSkills, opt => opt.Ignore());
            CreateMap<LanguageSkillRequest, LanguageSkill>();
            CreateMap<ExperienceRequest, Experience>();

            CreateMap<EducationRequest, Education>()
                .ForMember(dest => dest.QualificationTypes, opt => opt.Ignore())
                .ForMember(dest => dest.LevelOfQualifications, opt => opt.Ignore())
                .ForMember(dest => dest.Awards, opt => opt.Ignore());
            CreateMap<BankAccountRequest, BankAccount>();
            CreateMap<EmergencyContactRequest, EmergencyContact>();
            CreateMap<RepresentativeRequest, Representative>();
            CreateMap<WitnessRequest, Witness>();
            CreateMap<BeneficiaryRequest, Beneficiary>();
            CreateMap<AddressRequest, Address>();
            CreateMap<UpdateAddressRequest, Address>();
            CreateMap<RepAddressRequest, Address>();

            // Get Single Applicant Mapper
            CreateMap<Applicant, GetApplicantResponseDTO>()
                .ForMember(dest => dest.AttachmentFiles, opt => opt.Ignore());
            CreateMap<LanguageSkill, GetLanguageSkillResponseDTO>();
            CreateMap<Experience, GetExperienceResponseDTO>();
            CreateMap<Education, GetEducationResponseDTO>();
            CreateMap<BankAccount, GetBankAccountResponseDTO>();
            CreateMap<EmergencyContact, GetEmergencyContactResponseDTO>();
            CreateMap<Representative, GetRepresentativeResponseDTO>();
            CreateMap<Witness, GetWitnessResponseDTO>();
            CreateMap<Beneficiary, GetBeneficiaryResponseDTO>();
            CreateMap<Address, GetAddressResponseDTO>();
            CreateMap<Beneficiary, GetBeneficiaryResponseDTO>();
            CreateMap<Skill, GetSkillResponseDTO>();
            CreateMap<QualificationType, GetQualificationTypeResponseDTO>();
            CreateMap<LevelOfQualification, GetLevelOfQualificationResponseDTO>();
            CreateMap<Award, GetAwardResponseDTO>();

            // Edit Applicant Mapper
            CreateMap<EditApplicantRequest, Applicant>()
                .ForMember(dest => dest.Skills, opt => opt.Ignore());
            CreateMap<EditBankAccountRequest, BankAccount>();
            CreateMap<EditBeneficiaryRequest, Beneficiary>();
            CreateMap<EditEducationRequest, Education>()
                .ForMember(dest => dest.QualificationTypes, opt => opt.Ignore())
                .ForMember(dest => dest.LevelOfQualifications, opt => opt.Ignore())
                .ForMember(dest => dest.Awards, opt => opt.Ignore());
            CreateMap<EditEmergencyContactRequest, EmergencyContact>();
            CreateMap<EditExperienceRequest, Experience>();
            CreateMap<EditLanguageSkillRequest, LanguageSkill>();
            CreateMap<EditRepresentativeRequest, Representative>();
            CreateMap<EditWitnessRequest, Witness>();
            CreateMap<EditAddressRequest, Address>();
            CreateMap<EditRepAddressRequest, Address>();

            // Applicant Response
            CreateMap<Applicant, CreateApplicantResponseDTO>();
            CreateMap<SearchModel<Applicant>, SearchModel<ApplicantsResponseDTO>>();
            CreateMap<BankAccount, BankAccountResponseDTO>();
            CreateMap<Beneficiary, BeneficiaryResponseDTO>();
            CreateMap<Education, EducationResponseDTO>();
            CreateMap<EmergencyContact, EmergencyContactResponseDTO>();
            CreateMap<LanguageSkill, LanguageResponseDTO>();
            CreateMap<Partner, PartnerApplRespDTO>();
            CreateMap<Representative, RepresentativeResponseDTO>();
            CreateMap<Witness, WitnessResponseDTO>();
            CreateMap<Applicant, ApplicantsResponseDTO>()
                .ForMember(dest => dest.MaritalStatus, opt => opt.Ignore())
                .ForMember(dest => dest.Religion, opt => opt.Ignore())
                .ForMember(dest => dest.BrokerName, opt => opt.Ignore());
            CreateMap<Applicant, ApplSearchResponseDTO>();
            CreateMap<SearchModel<Applicant>, SearchModel<ApplSearchResponseDTO>>();

            CreateMap<CreatePartnerRequest, Partner>();
            CreateMap<Partner, PartnerResponseDTO>();
            CreateMap<Address, AddressResponseDTO>();

            CreateMap<CreateOrderRequest, Order>()
            .ForMember(dest => dest.Employees, opt => opt.Ignore())
            .ForMember(dest => dest.Payment, opt => opt.Ignore());
            CreateMap<OrderCriteriaRequest, OrderCriteria>();
            CreateMap<SponsorRequest, Sponsor>();
            CreateMap<SponsorAddressRequest, Address>();

            // Map Group of orders
            CreateMap<Order, GetOrdersResponseDTO>();
            CreateMap<Applicant, OrderApplResponseDTO>();
            CreateMap<OrderCriteria, OrderCriteriaResponseDTO>();
            CreateMap<Payment, PaymentResponseDTO>();
            CreateMap<Sponsor, SponsorResponseDTO>();
            CreateMap<SearchModel<Order>, SearchModel<GetOrdersResponseDTO>>();

            // Map response to get single Order
            CreateMap<Order, GetOrderRespDTO>()
                .ForMember(dest => dest.Attachment, opt => opt.Ignore());
            CreateMap<OrderCriteria, GetOrderCriteriaRespDTO>();
            CreateMap<Payment, GetPaymentRespDTO>();
            CreateMap<Sponsor, GetSponsorRespDTO>()
                .ForMember(dest => dest.Attachment, opt => opt.Ignore());
            CreateMap<Address, GetSponsorAddressRespDTO>();
            CreateMap<ServiceResponse<Order>, ServiceResponse<GetOrderRespDTO>>();
            CreateMap<Order, GetForAssignmentOrderDTO>();
            CreateMap<OrderCriteria, GetOrderCriteriaRespDTO>();
            CreateMap<Sponsor, GetSponsorResponseDTO>();
            CreateMap<Applicant, GetApplForAssignmentDTO>();

            CreateMap<EditOrderRequest, Order>()
                .ForMember(dest => dest.Employees, opt => opt.Ignore());
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
            CreateMap<EditApplicantFollowupStatusCommand, ApplicantFollowupStatus>();

            CreateMap<ApplicantFollowupStatus, GetApplicantFollowupStatusByIdResponseDTO>();
            CreateMap<EditApplicantFollowupStatusCommand, ApplicantFollowupStatus>();

            CreateMap<SearchModel<LookUp>, SearchModel<LookUpResponseDTO>>();
            CreateMap<SearchModel<Attachment>, SearchModel<AttachmentResponseDTO>>();
            CreateMap<SearchModel<OnlineApplicant>, SearchModel<OnlineApplicantResponseDTO>>();

            CreateMap<SearchModel<Page>, SearchModel<PageResponseDTO>>();
            CreateMap<CreatePageRequest, Page>();
            CreateMap<Page, PageResponseDTO>();
            CreateMap<EditPageCommand, Page>();
            CreateMap<Page, GetPageByIdResponseDTO>();

            CreateMap<CreateCompanyInformationRequest, CompanyInformation>()
                .ForMember(dest => dest.Witnesses, opt => opt.Ignore()).ReverseMap();
            CreateMap<EditCompanyInformationCommand, CompanyInformation>()
                 .ForMember(dest => dest.Witnesses, opt => opt.Ignore()).ReverseMap();
            CreateMap<Address, CompanyAddressRequest>().ReverseMap();
            //CreateMap<CreateCompanyInformationCommand, CompanyInformation>().ReverseMap();

            CreateMap<CompanySetting, CompanySettingRequest>().ReverseMap();
            CreateMap<Witness, WitnessRequest>().ReverseMap();
            CreateMap<CompanyWitnessRequest, Witness>().ReverseMap();
            CreateMap<CompanyInformation, CompanyInformationResponseDTO>();
            CreateMap<CompanyInformation, GetCompanyInformationResponseDTO>()
                .ForMember(dest => dest.LetterLogo, opt => opt.Ignore())
                .ForMember(dest => dest.LetterBackGround, opt => opt.Ignore());
            CreateMap<Address, CompanyAddressResponseDTO>();
            CreateMap<CompanyInformation, GetCompanyInformationResponseDTO>();
            CreateMap<EditCompanyAddressRequest, Address>();
            CreateMap<EditCompanyInformationCommand, CompanyInformation>().ReverseMap();
            CreateMap<EditCompanyWitnessRequest, Witness>();
            CreateMap<EditCountryOperationRequest, CountryOperation>();
            CreateMap<EditCompanySettingRequest, CompanySetting>().ReverseMap();

            CreateMap<CreateCompanyInformationRequest, CompanyInformation>().ReverseMap();
            CreateMap<Address, CompanyAddressRequest>().ReverseMap();
            CreateMap<CompanySetting, CompanySettingRequest>().ReverseMap();
            CreateMap<Witness, WitnessRequest>().ReverseMap();
            CreateMap<CountryOperationRequest, CountryOperation>();
            CreateMap<CompanyInformation, CompanyInformationResponseDTO>();

            CreateMap<Process, GetProcessResponseDTO>();
            CreateMap<Process, ProcessDetailsResponseDTO>();
            CreateMap<ProcessDefinition, ProcessDefinitionResponseDTO>();
            CreateMap<ProcessDefinition, GetProcessDefinitionResponseDTO>();
            CreateMap<ProcessDefinition, GetPDResponseDTO>();
            CreateMap<ApplicantProcess, GetApplProcessResponseDTO>();
            CreateMap<SearchModel<ApplicantProcess>, SearchModel<GetApplProcessResponseDTO>>();

            CreateMap<CreateProcessRequest, Process>();
            CreateMap<CreateProcessDefinitionRequest, ProcessDefinition>();
            CreateMap<EditProcessRequest, Process>();
            CreateMap<EditProcessDefinitionRequest, ProcessDefinition>();
            CreateMap<EditLookUpCommand, LookUp>();
            CreateMap<AddEnjazRequest, Enjaz>();

            CreateMap<ProcessDefinition, GetProcessDefinitionResponseDTO>();
            CreateMap<AddUserRequest, ApplicationUser>()
                .ForMember(dest => dest.UserGroups, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.Ignore());
            CreateMap<UpdateUserRequest, ApplicationUser>()
                .ForMember(dest => dest.UserGroups, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.Ignore());
            CreateMap<ApplicationUser, UserResponseDTO>();
            CreateMap<ApplicationUser, UserDetailsResponseDTO>();
            CreateMap<SearchModel<ApplicationUser>, SearchModel<UserResponseDTO>>();
            CreateMap<SearchModel<UserGroup>, SearchModel<FetchGroupDTO>>();
            CreateMap<UserGroup, FetchGroupDTO>();
            CreateMap<UserGroup, GroupDTO>();
            CreateMap<Permission, PermissionDto>();
            CreateMap<PermissionRequest, Permission>();

        }
    }
}
