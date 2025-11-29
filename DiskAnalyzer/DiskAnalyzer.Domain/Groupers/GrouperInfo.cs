namespace DiskAnalyzer.Domain.Groupers;

public record GrouperInfo(string Type);

[AttributeUsage(AttributeTargets.Class)]
public class GrouperTypeAttribute(string displayName) : Attribute
{
    public string DisplayName { get; } = displayName;
}
