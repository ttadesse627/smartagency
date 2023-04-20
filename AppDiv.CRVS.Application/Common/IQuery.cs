using MediatR;

namespace AppDiv.CRVS.Application.Common
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
