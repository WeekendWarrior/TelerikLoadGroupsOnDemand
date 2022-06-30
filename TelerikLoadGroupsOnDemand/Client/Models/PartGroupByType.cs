namespace TelerikLoadGroupsOnDemand.Client.Models;

public enum PartGroupByType
{
    None = 0,

    [ColumnMember(nameof(Part.CustomerName), typeof(string), true)]
    CustomerName = 1,

    [ColumnMember(nameof(Part.Cipn), typeof(string), true)]
    Cipn = 2,
}