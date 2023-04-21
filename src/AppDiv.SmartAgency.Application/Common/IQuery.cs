using MediatR;

namespace AppDiv.SmartAgency.Application.Common
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
