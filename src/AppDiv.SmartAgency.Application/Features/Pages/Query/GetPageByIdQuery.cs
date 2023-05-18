using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.DTOs.PageDTOs;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Application.Mapper;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.Pages.Query
{
    public class GetPageByIdQuery: IRequest<PageResponseDTO>
    {
        public Guid Id { get; private set; }

        public GetPageByIdQuery(Guid Id)
        {
            this.Id = Id;
        }

    }

    public class GetPageByIdHandler : IRequestHandler<GetPageByIdQuery, PageResponseDTO>
    {
        private readonly IMediator _mediator;
       private readonly IPageRepository _pageRepository;
        public GetPageByIdHandler(IMediator mediator, IPageRepository pageRepository)
        {
            _mediator = mediator;
            _pageRepository= pageRepository;
        }
        public async Task<PageResponseDTO> Handle(GetPageByIdQuery request, CancellationToken cancellationToken)
        {
            //var pages = await _mediator.Send(new GetAllPagesQuery());
            var selectedPage = await _pageRepository.GetByIdAsync(request.Id);
            Console.WriteLine(selectedPage);
            return CustomMapper.Mapper.Map<PageResponseDTO>(selectedPage);
           
        }
    }
}