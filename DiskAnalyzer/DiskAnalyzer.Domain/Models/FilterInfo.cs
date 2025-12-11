namespace DiskAnalyzer.Domain.Models;

/// <summary>
/// Описание примененного фильтра в результатах анализа.
/// </summary>
/// <param name="Type">Тип фильтра (из <see cref="Attributes.FilterTypeAttribute"/> или имя класса).</param>
/// <param name="Parameters">
/// Параметры фильтра (из свойств с <see cref="Attributes.FilterInfoAttribute"/>).
/// </param>
public record FilterInfo(
    string Type,
    Dictionary<string, string> Parameters);