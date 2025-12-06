namespace DiskAnalyzer.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class FilterTypeAttribute(string displayName) : Attribute
{
    public string DisplayName { get; } = displayName;
}