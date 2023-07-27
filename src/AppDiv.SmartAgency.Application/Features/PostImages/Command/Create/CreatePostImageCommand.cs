using AppDiv.SmartAgency.Application.Contracts.Request.Pagess;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.PostImages.Command.Create
{
    public record CreatePostImageCommand(CreatePostImageRequest Request) : IRequest<string> { }
}