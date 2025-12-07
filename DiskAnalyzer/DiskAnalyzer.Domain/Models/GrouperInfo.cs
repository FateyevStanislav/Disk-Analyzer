namespace DiskAnalyzer.Domain.Models;

/// <summary>
/// Описание примененной стратегии группировки.
/// </summary>
/// <param name="Type">Тип группировщика (из <see cref="Attributes.GrouperTypeAttribute"/>).</param>
public record GrouperInfo(string Type);