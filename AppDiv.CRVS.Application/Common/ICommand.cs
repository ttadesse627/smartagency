using MediatR;

namespace AppDiv.CRVS.Application.Common
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
