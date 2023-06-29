

using System.Text.Json.Serialization;

namespace AppDiv.SmartAgency.Utility.Contracts;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SqlOperator
{
    Contain,
    GreaterThan,
    LessThan,
    EqualTo,
    Between,
    NotContain,
    LessThanOrEqual,
    GreaterThanOrEqual,
    NotEqual,
    NotBetween
}
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SqlAggregate
{
    GroupBy,
    OrderBy,
    Count,
    Max,
    Min,
    Average,
}