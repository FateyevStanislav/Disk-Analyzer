namespace DiskAnalyzer.Infrastructure.Filter;

[AttributeUsage(AttributeTargets.Property)]
public class FilterInfoAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}
