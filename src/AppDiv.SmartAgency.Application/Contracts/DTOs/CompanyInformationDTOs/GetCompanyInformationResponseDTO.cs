using AppDiv.SmartAgency.Application.Contracts.Request.CompanyInformations;


namespace AppDiv.SmartAgency.Application.Contracts.DTOs.CompanyInformationDTOs
{
    public record GetCompanyInformationResponseDTO
    {

        public Guid Id { get; set; }
        public string? CompanyName { get; set; }

        public string? CompanyNameAmharic { get; set; }

        public string? CompanyNameArabic { get; set; }

        public string? ContractNumber { get; set; }

        public string? LicenseNumber { get; set; }
        public string? AssurancePolicyNumber { get; set; }

        public string? GeneralManager { get; set; }
        public string? GeneralManagerAmharic { get; set; }
        public string? ViceManager { get; set; }
        public string? ViceManagerAmharic { get; set; }
        public string? CountriesOperation { get; set; }
        public string? LetterLogo { get; set; }
        public string? LetterBackGround { get; set; }
        public CompanyAddressResponseDTO? Address { get; set; }

        public CompanyWitnssRequest? Witness { get; set; }
        public ICollection<CountryOperationResponseDTO>? CountryOperations { get; set; }
        public CompanySettingRequest? CompanySetting { get; set; }

    }
}