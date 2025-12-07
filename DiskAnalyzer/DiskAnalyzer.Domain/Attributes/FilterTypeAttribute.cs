namespace DiskAnalyzer.Domain.Attributes;

/// <summary>
/// Отмечает свойство фильтра для включения в параметры <see cref="Models.FilterInfo"/>.
/// </summary>
/// <remarks>
/// Применяется к public свойствам <see cref="Abstractions.IFileFilter"/>.
/// Значение свойства сериализуется в строку через ToString().
/// </remarks>
[AttributeUsage(AttributeTargets.Class)]
public class FilterTypeAttribute(string displayName) : Attribute
{
    public string DisplayName { get; } = displayName;
}