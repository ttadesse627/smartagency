

namespace AppDiv.SmartAgency.Application.Contracts.DTOs.Common;
public record ResponseContainerDTO<T>
{
    public T? Items { get; set; }
}