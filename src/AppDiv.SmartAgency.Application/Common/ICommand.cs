using MediatR;

namespace AppDiv.SmartAgency.Application.Common
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
