namespace DiskAnalyzer.Infrastructure.Filter;

[AttributeUsage(AttributeTargets.Class)]
public class FilterTypeAttribute(string displayName) : Attribute
{
    public string DisplayName { get; } = displayName;
}