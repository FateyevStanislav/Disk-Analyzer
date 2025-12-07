namespace DiskAnalyzer.Domain.Models;

/// <summary>
/// Группа файлов с вычисленными метриками.
/// </summary>
/// <param name="Key">Ключ группы (например, ".pdf", "1MB-10MB").</param>
/// <param name="TotalSize">Суммарный размер всех файлов в группе.</param>
/// <param name="FilesCount">Количество файлов в группе.</param>
/// <param name="Files">Список файлов в группе.</param>
public sealed record FileGroup(
    string Key,
    long TotalSize,
    int FilesCount,
    IReadOnlyList<FileDetails> Files);