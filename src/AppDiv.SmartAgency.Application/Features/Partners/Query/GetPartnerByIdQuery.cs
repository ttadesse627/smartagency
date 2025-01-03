using AppDiv.SmartAgency.Application.Contracts.DTOs.PartnersDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Partners.Query
{
    public record GetPartnerByIdQuery(Guid Id) : IRequest<PartnerResponseDTO> { }

    public class GetPartnerByIdHandler(IPartnerRepository partnerRepository, IFileService fileService) : IRequestHandler<GetPartnerByIdQuery, PartnerResponseDTO>
    {
        private readonly IPartnerRepository _partnerRepository = partnerRepository;
        private readonly IFileService _fileService = fileService;

        public async Task<PartnerResponseDTO> Handle(GetPartnerByIdQuery request, CancellationToken cancellationToken)
        {
            var selectedPartner = await _partnerRepository.GetByIdAsync(request.Id);
            var headerLogoId = request.Id.ToString();

            var folder = "PartnersHeaderLogo";
            (byte[], string, string) fileResult = _fileService.getFile(headerLogoId, folder, null);

            // Convert the byte array of the image content to a Base64 encoded string
            string fileContent = Convert.ToBase64String(fileResult.Item1);

            // Create an anonymous object with properties "FileName" and "FileContent"
            var response = new { FileName = fileResult.Item2 + fileResult.Item3, FileContent = fileContent };

            //can use this instead of automapper
            var partnerResponseDTO = new PartnerResponseDTO()
            {
                Id = selectedPartner!.Id,
                PartnerType = selectedPartner.PartnerType,
                PartnerName = selectedPartner.PartnerName,
                PartnerNameAmharic = selectedPartner.PartnerNameAmharic!,
                PartnerNameArabic = selectedPartner.PartnerNameArabic!,
                ContactPerson = selectedPartner.ContactPerson,
                IdNumber = selectedPartner.IdNumber!,
                ManagerNameAmharic = selectedPartner.ManagerNameAmharic!,
                LicenseNumber = selectedPartner.LicenseNumber!,
                BankName = selectedPartner.BankName!,
                BankAccount = selectedPartner.BankAccount!,
                HeaderLogo = fileContent,
                ReferenceNumber = selectedPartner.ReferenceNumber!,
                Address = CustomMapper.Mapper.Map<PartnerAddressResponseDTO>(selectedPartner.Address)
            };

            return partnerResponseDTO;

        }
    }
}