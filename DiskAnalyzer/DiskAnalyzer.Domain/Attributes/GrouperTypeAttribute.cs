namespace DiskAnalyzer.Domain.Attributes;

/// <summary>
/// Указывает отображаемое имя стратегии группировки.
/// </summary>
/// <remarks>
/// Применяется к классам, реализующим <see cref="Abstractions.IFileGrouper"/>.
/// </remarks>
[AttributeUsage(AttributeTargets.Class)]
public class GrouperTypeAttribute(string displayName) : Attribute
{
    public string DisplayName { get; } = displayName;
}
