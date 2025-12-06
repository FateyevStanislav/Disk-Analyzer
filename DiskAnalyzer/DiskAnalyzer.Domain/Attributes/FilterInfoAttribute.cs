namespace DiskAnalyzer.Domain.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class FilterInfoAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}
