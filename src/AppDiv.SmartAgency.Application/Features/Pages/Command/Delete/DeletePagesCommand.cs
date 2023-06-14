using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Pages.Command.Delete
{
    public class DeletePagesCommand: IRequest<String>
    {
        public IEnumerable<Guid> Ids { get; set; }


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
            try
            {
              //  var pageEntity = await _pageRepository.GetByIdAsync(request.Id);
          
                await _pageRepository.DeleteManyAsync((IEnumerable<object>)request.Ids);
                 await _pageRepository.SaveChangesAsync(cancellationToken);


            }
            catch (Exception exp)
            {
                throw (new ApplicationException(exp.Message));
            }

            return "Web page informations have been deleted!";
        }
    }
}