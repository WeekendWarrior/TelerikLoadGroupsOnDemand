using Telerik.DataSource;

namespace TelerikLoadGroupsOnDemand.Shared;

public class ServiceResponse<T>
{
    public bool Success { get; set; }

    public string? Message { get; set; }

    public T? Data { get; set; }

    public List<AggregateFunctionsGroup> GroupedData { get; set; } = new();

    public int TotalResults { get; set; }
}