namespace DiskAnalyzer.Domain.Models;

/// <summary>
/// Метаданные файла для результатов анализа.
/// </summary>
/// <param name="FullPath">Абсолютный путь к файлу.</param>
/// <param name="Size">Размер файла в байтах.</param>
public record FileDetails(string Path, long Size);