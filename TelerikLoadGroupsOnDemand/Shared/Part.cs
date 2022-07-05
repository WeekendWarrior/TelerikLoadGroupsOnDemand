namespace TelerikLoadGroupsOnDemand.Shared;

public record Part
{
    public string Mpn { get; init; } = string.Empty;

    public string Cipn { get; init; } = string.Empty;

    public string CustomerName { get; init; } = string.Empty;

    public int? CustomerNumber { get; init; }
}