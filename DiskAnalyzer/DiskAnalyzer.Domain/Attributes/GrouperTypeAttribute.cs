namespace DiskAnalyzer.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class GrouperTypeAttribute(string displayName) : Attribute
{
    public string DisplayName { get; } = displayName;
}
