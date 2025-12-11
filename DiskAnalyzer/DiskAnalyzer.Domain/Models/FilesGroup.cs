namespace DiskAnalyzer.Domain.Models;

/// <summary>
/// Группа файлов с вычисленными метриками.
/// </summary>
/// <param name="Key">Ключ группы (например, ".pdf", "1MB-10MB").</param>
/// <param name="Measurements">
/// Словарь метрик: ключ = тип измерения, значение = результат.
/// Примеры: { "TotalSize": "104857600", "FilesCount": "42" }
/// <param name="Files">Список файлов в группе.</param>
public sealed record FilesGroup(
    string Key,
    Dictionary<string, long> Metrics,  
    IReadOnlyCollection<FileDetails> Files
);