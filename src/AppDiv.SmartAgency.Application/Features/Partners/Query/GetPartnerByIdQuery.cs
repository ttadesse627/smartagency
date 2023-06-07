using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Exceptions;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.Application.Features.Partners.Query
{
    // Customer GetCustomerByIdQuery with Customer response
    public class GetPartnerByIdQuery : IRequest<PartnerResponseDTO>
    {
        public Guid Id { get; private set; }
        public string fileType { get; private set; }
        public string folderType { get; private set; }

        public GetPartnerByIdQuery(Guid Id, string fileType, string folderType)
        {
            this.Id = Id;
            this.fileType = fileType;
            this.folderType = folderType;
            this.fileType = fileType;
            this.folderType = folderType;
        }

    }

    public class GetPartnerByIdHandler : IRequestHandler<GetPartnerByIdQuery, PartnerResponseDTO>
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly IFileService _fileService;

        public GetPartnerByIdHandler(IPartnerRepository partnerRepository, IFileService fileService)
        {
            _partnerRepository = partnerRepository;
            _fileService = fileService;
            _fileService = fileService;
        }
        public async Task<PartnerResponseDTO> Handle(GetPartnerByIdQuery request, CancellationToken cancellationToken)
        {
            var selectedPartner = await _partnerRepository.GetByIdAsync(request.Id);
            // var postImageId= "postImage" + id.ToString();
            var headerLogoId = request.Id.ToString();

            var fileType = "PartnersHeaderLogo";

            //string fileName = "Slider" + id.ToString() + ".jpg"; // Replace ".jpg" with the actual file extension
            (byte[], string, string) fileResult = _fileService.getFile(headerLogoId, request.fileType, request.folderType);

            // Convert the byte array of the image content to a Base64 encoded string
            string fileContent = Convert.ToBase64String(fileResult.Item1);

            // Create an anonymous object with properties "FileName" and "FileContent"
            var response = new { FileName = fileResult.Item2 + fileResult.Item3, FileContent = fileContent };

            //can use this instead of automapper
            var partnerResponseDTO = new PartnerResponseDTO()
            {
                Id = selectedPartner.Id,
                PartnerType = selectedPartner.PartnerType,
                PartnerName = selectedPartner.PartnerName,
                PartnerNameAmharic = selectedPartner.PartnerNameAmharic,
                PartnerNameArabic = selectedPartner.PartnerNameArabic,
                ContactPerson = selectedPartner.ContactPerson,
                IdNumber = selectedPartner.IdNumber,
                ManagerNameAmharic = selectedPartner.ManagerNameAmharic,
                LicenseNumber = selectedPartner.LicenseNumber,
                BankName = selectedPartner.BankName,
                BankAccount = selectedPartner.BankAccount,
                HeaderLogo = fileContent,
                ReferenceNumber = selectedPartner.ReferenceNumber,
                Address = CustomMapper.Mapper.Map<PartnerAddressResponseDTO>(selectedPartner.Address)
            };

            return partnerResponseDTO;

        }
    }
}