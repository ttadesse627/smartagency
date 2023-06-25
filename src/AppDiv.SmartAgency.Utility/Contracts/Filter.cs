

namespace AppDiv.SmartAgency.Utility.Contracts;
public class Filter
{
    public string? PropertyName { get; set; }
    public SqlOperator? Operator { get; set; } = SqlOperator.Contain;
    public object? Value { get; set; }
}