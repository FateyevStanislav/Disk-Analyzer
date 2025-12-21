namespace DiskAnalyzer.Domain.Models;

/// <summary>
/// Группа идентичных файлов (дубликаты).
/// </summary>
/// <param name="FileHash">SHA256 хеш содержимого в hex-формате.</param>
/// <param name="FileSize">Размер одного файла в байтах.</param>
/// <param name="FileCount">Количество файлов в группе (минимум 2).</param>
/// <param name="TotalWastedSpace">
/// Потраченное пространство = FileSize * (FileCount - 1).
/// </param>
/// <param name="Files">Список всех файлов в группе.</param>
public record DuplicateGroup(
    string FileHash,
    long FileSize,
    int FileCount,
    long TotalWastedSpace,
    FileDetails OriginalFile,  
    IReadOnlyList<FileDetails> Files);