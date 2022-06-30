namespace TelerikLoadGroupsOnDemand.Client.Models;

public class ColumnMemberAttribute : Attribute
{
    public string MemberName { get; }

    public Type? MemberType { get; }

    public bool SortAscending { get; }

    public ColumnMemberAttribute(string memberName)
    {
        MemberName = memberName;
        MemberType = typeof(string);
        SortAscending = true;
    }

    public ColumnMemberAttribute(string memberName, Type? memberType)
    {
        MemberName = memberName;
        MemberType = memberType;
        SortAscending = true;
    }

    public ColumnMemberAttribute(string memberName, Type? memberType, bool sortAscending)
    {
        MemberName = memberName;
        MemberType = memberType;
        SortAscending = sortAscending;
    }
}


public static class ColumnMemberAttributeExtensions
{
    public static string ToMemberName(this Enum en)
    {
        var type = en.GetType();

        var memInfo = type.GetMember(en.ToString());

        if (memInfo.Length <= 0) return en.ToString();

        var attrs = memInfo[0].GetCustomAttributes(typeof(ColumnMemberAttribute), false);

        return attrs.Length > 0 
            ? ((ColumnMemberAttribute)attrs[0]).MemberName 
            : en.ToString();
    }

    public static Type? ToMemberType(this Enum en)
    {
        var type = en.GetType();

        var memInfo = type.GetMember(en.ToString());

        if (memInfo.Length <= 0) return default;

        var attrs = memInfo[0].GetCustomAttributes(typeof(ColumnMemberAttribute), false);

        return attrs.Length > 0
            ? ((ColumnMemberAttribute) attrs[0]).MemberType
            : default;
    }

    public static ListSortDirection ToListSortDirection(this Enum en)
    {
        var type = en.GetType();

        var memInfo = type.GetMember(en.ToString());

        if (memInfo.Length <= 0) return default;

        var attrs = memInfo[0].GetCustomAttributes(typeof(ColumnMemberAttribute), false);

        var sortAsc = attrs.Length <= 0 || ((ColumnMemberAttribute) attrs[0]).SortAscending;

        return sortAsc ? ListSortDirection.Ascending : ListSortDirection.Descending;
    }
}