using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Application.Contracts.Request.Pagess;
using MediatR;

namespace AppDiv.SmartAgency.Application.Features.PostImages.Command.Create
{
 public record CreatePostImageCommand(CreatePostImageRequest image) : IRequest<string>
{

}
}