namespace DiskAnalyzer.Domain.Abstractions;

/// <summary>
/// Определяет условие фильтрации файлов при обходе файловой системы.
/// </summary>
public interface IFileFilter
{
    /// <summary>
    /// Определяет, должен ли файл быть включён в результаты сканирования.
    /// </summary>
    /// <param name="file">Файл для проверки.</param>
    /// <returns>
    /// <c>true</c>, если файл проходит фильтр и должен быть обработан;
    /// <c>false</c>, если файл должен быть пропущен.
    /// </returns>
    bool ShouldInclude(FileInfo file);
}