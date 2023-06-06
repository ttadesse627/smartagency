using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Partners.Command.Delete
{
    public class DeletePartnerCommand: IRequest<String>
    {
        public Guid Id { get; private set; }

        public DeletePartnerCommand(Guid Id)
        {
            this.Id = Id;
        }
    }

   
    // lookUp delete command handler with string response as output
    public class DeletePartnerCommmandHandler : IRequestHandler<DeletePartnerCommand, String>
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IFileService _fileService;

        public DeletePartnerCommmandHandler(IPartnerRepository partnerRepository, IAddressRepository addressRepository, IFileService fileService)
        {
             _fileService = fileService;
            _partnerRepository= partnerRepository;
            _addressRepository= addressRepository;
        }

        public async Task<string> Handle(DeletePartnerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var partnerEntity = await _partnerRepository.GetByIdAsync(request.Id);
                var AddressId=partnerEntity.AddressId; 
                await _partnerRepository.DeleteAsync(partnerEntity.Id);
                await _addressRepository.DeleteAsync(AddressId);
                 await _partnerRepository.SaveChangesAsync(cancellationToken);
                  await _addressRepository.SaveChangesAsync(cancellationToken);

        string fileName = request.Id.ToString() + "*"; // Replace "." with the actual file extension
        var response = _fileService.DeleteFile(fileName, "PartnersHeaderLogo");

  

            }
            catch (Exception exp)
            {
                throw (new ApplicationException(exp.Message));
            }

            return "Partner information has been deleted!";
        }
    }
}
 

