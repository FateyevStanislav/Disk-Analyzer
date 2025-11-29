namespace DiskAnalyzer.Domain.Filters;

public record FilterInfo(
    string Type,
    Dictionary<string, string> Parameters);

[AttributeUsage(AttributeTargets.Property)]
public class FilterInfoAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}

[AttributeUsage(AttributeTargets.Class)]
public class FilterTypeAttribute(string displayName) : Attribute
{
    public string DisplayName { get; } = displayName;
}