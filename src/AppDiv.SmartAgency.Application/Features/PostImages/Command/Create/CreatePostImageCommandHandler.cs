using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.PostImages.Command.Create
{
    public class CreatePostImageCommandHandler : IRequestHandler<CreatePostImageCommand, string>
{
        private readonly IPartnerRepository _partnerRepository;
        private readonly IFileService _fileService;

       public CreatePostImageCommandHandler(IFileService fileService, IPartnerRepository partnerRepository)
        {
            _fileService = fileService;
            _partnerRepository= partnerRepository;
        }
        public async Task<string> Handle(CreatePostImageCommand request, CancellationToken cancellationToken)
        {
           
                var file = request.image.PostImage;
                var folderName = Path.Combine("Resources", "Sliders");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                Guid imgId=Guid.NewGuid();
                var fileName = "postImage"+imgId;
              //var  fileName="postImage1a2f32ad-63e7-4a4a-b665-20871842c978";

        
                if(!string.IsNullOrEmpty(file)){

                await _fileService.UploadBase64FileAsync(file, fileName, pathToSave, FileMode.Create);
                }

              return "sucessfully inserted";
            }
            
        }
}
