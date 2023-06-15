

using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Pages.Command.Delete
{
    public class DeletePageCommand: IRequest<String>
    {
        public Guid Id { get; private set; }

        public DeletePageCommand(Guid Id)
        {
            this.Id = Id;
        }
    }

   
    // lookUp delete command handler with string response as output
    public class DeletePageCommmandHandler : IRequestHandler<DeletePageCommand, String>
    {
        private readonly IPageRepository _pageRepository;
      
        public DeletePageCommmandHandler(IPageRepository pageRepository)
        {
            _pageRepository= pageRepository;
        }

        public async Task<string> Handle(DeletePageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var pageEntity = await _pageRepository.GetByIdAsync(request.Id);
          
                await _pageRepository.DeleteAsync(pageEntity.Id);
                 await _pageRepository.SaveChangesAsync(cancellationToken);


            }
            catch (Exception exp)
            {
                throw (new ApplicationException(exp.Message));
            }

            return "Web page information has been deleted!";
        }
    }
}