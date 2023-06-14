using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Pages.Command.Delete
{
    public record DeletePagesCommand(List<Guid> Ids): IRequest<String>
    {
    }

   
    // lookUp delete command handler with string response as output
    public class DeletePagesCommmandHandler : IRequestHandler<DeletePagesCommand, String>
    {
        private readonly IPageRepository _pageRepository;
      
        public DeletePagesCommmandHandler(IPageRepository pageRepository)
        {
            _pageRepository= pageRepository;
        }

        public async Task<string> Handle(DeletePagesCommand request, CancellationToken cancellationToken)
        {
              int response=  0;
            try
            {
                //var pageEntity = await _pageRepository.GetByIdAsync(request.Id);
          
              response= await _pageRepository.DeleteMany(request.Ids);
                


            }
            catch (Exception exp)
            {
                throw (new ApplicationException(exp.Message));
            }

            return response+ " "+"records deleted ";
        }
    }
}