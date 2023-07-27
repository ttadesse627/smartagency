using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using FluentValidation;

namespace AppDiv.SmartAgency.Application.Features.Partners.Command.Create
{
    public class CreatePartnerCommandValidator : AbstractValidator<CreatePartnerCommand>
    {
        private readonly IPartnerRepository _repo;
        public CreatePartnerCommandValidator(IPartnerRepository repo)
        {
            _repo = repo;
            RuleFor(p => p.partner.PartnerType)
                .NotEmpty().WithMessage("Partner Type is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("Partner type must not exceed 50 characters.");
            RuleFor(p => p.partner.PartnerName)
                .NotEmpty().WithMessage("Partner Name is required")
                .NotNull().WithMessage("Partner Name must has a value")
                .MaximumLength(100).WithMessage("Partner Name must not exceed 100 characters");
            RuleFor(p => p.partner.ContactPerson)
                .NotEmpty().WithMessage("Contact Person is required")
                .NotNull().WithMessage("Contact Person must has a value")
                .MaximumLength(100).WithMessage("Contact Person must not exceed 100 characters");
            RuleFor(p => p.partner.Address.CountryId)
                .NotEmpty().WithMessage("Country is required")
                .NotNull().WithMessage("Country must has a value");
            RuleFor(p => p.partner.Address.CityId)
               .NotEmpty().WithMessage("City  is required")
               .NotNull().WithMessage("City must has a value");
            // RuleFor(p => p.partner.HeaderLogo)
            //     .NotEmpty().WithMessage






        }


        private bool BeValidBase64Image(string image)
        {
            if (string.IsNullOrEmpty(image))
                return false;

            // Check if the string starts with the base64 image header
            if (!image.StartsWith("data:image/"))
                return false;

            try
            {
                // Extract the content type and base64 payload from the image string
                var parts = image.Split(',');
                var contentType = parts[0].Split(':')[1].Split(';')[0].Trim();
                var base64Payload = parts[1];

                // Convert the base64 payload to bytes
                //var imageBytes = Convert.FromBase64String(base64Payload);

                // Perform additional checks if necessary, such as validating image dimensions or file size

                // Return true if all checks pass
                return true;
            }
            catch
            {
                // An exception occurred, indicating the image is not valid
                return false;
            }
        }






    }
}