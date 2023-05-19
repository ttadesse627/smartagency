using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PageDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using AppDiv.SmartAgency.Domain.Entities;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Pages.Command.Update
{
    public class EditPageCommand: IRequest<PageResponseDTO>
    {
      public Guid Id {get; set;} 
      public string  Category {get; set;}  
      public string  Link {get; set;}
      public string  Title{get; set;}

      public string  PageContent{get; set;}
     

       
    }



   

    public class EditPageCommandHandler : IRequestHandler<EditPageCommand, PageResponseDTO>
    {
   
        private readonly IPageRepository _pageRepository;
        private readonly IPageRepository _pageQueryRepository;
        public EditPageCommandHandler(IPageRepository pageRepository,IPageRepository pageQueryRepository)
        {
            _pageRepository = pageRepository;
            _pageQueryRepository=pageQueryRepository;
        }
        public async Task<PageResponseDTO> Handle(EditPageCommand request,  CancellationToken cancellationToken)
        {
            var pageResponse = new PageResponseDTO();
          
            var pageEntity = CustomMapper.Mapper.Map<Page>(request);
            try
            {
                var res =    await _pageRepository.UpdateAsync(pageEntity);
                // await _partnerRepository.SaveChangesAsync(cancellationToken);

                if(res>=1) {

                    var modifiedPage = await _pageQueryRepository.GetByIdAsync(request.Id);
                    pageResponse = CustomMapper.Mapper.Map<PageResponseDTO>(modifiedPage);
                 
                }
            }
            catch (Exception exp)
            {
                throw new ApplicationException(exp.Message);
            }
           
     
          //  var modifiedPartner = await _partnerQueryRepository.GetByIdAsync(request.Id);
           // cd cvar partnerResponse = CustomMapper.Mapper.Map<PartnerResponseDTO>(modifiedPartner);

            return pageResponse;
        }
    }
}