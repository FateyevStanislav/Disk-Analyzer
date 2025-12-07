namespace DiskAnalyzer.Domain.Attributes;

/// <summary>
/// Указывает отображаемое имя типа фильтра для сериализации в <see cref="Models.FilterInfo"/>.
/// </summary>
/// <remarks>
/// Применяется к классам, реализующим <see cref="Abstractions.IFileFilter"/>.
/// Если отсутствует, используется имя класса без суффикса "Filter".
/// </remarks>
[AttributeUsage(AttributeTargets.Property)]
public class FilterInfoAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}
