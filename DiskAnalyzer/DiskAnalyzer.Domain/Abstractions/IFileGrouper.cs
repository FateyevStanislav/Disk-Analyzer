namespace DiskAnalyzer.Domain.Abstractions;

/// <summary>
/// Определяет стратегию группировки файлов по ключу.
/// </summary>
/// <remarks>
/// Используется совместно с <see cref="Services.FilesGrouper"/> для категоризации файлов.
/// Реализации должны возвращать стабильные ключи для одинаковых файлов.
/// </remarks>
public interface IFileGrouper
{
    /// <summary>
    /// Вычисляет ключ группы для заданного файла.
    /// </summary>
    /// <param name="file">Файл для группировки.</param>
    /// <returns>
    /// Строковый ключ группы. Файлы с одинаковым ключом попадают в одну группу.
    /// </returns>
    string GetKey(FileInfo file);
}
