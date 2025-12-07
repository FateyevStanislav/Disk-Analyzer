namespace DiskAnalyzer.Domain.Abstractions;

/// <summary>
/// Выполняет обход файловой системы с применением фильтров.
/// </summary>
/// <remarks>
/// Абстракция для изоляции доменной логики от деталей файловой системы.
/// </remarks>
public interface IFileSystemScanner
{
    /// <summary>
    /// Сканирует файловую систему начиная с указанного пути.
    /// </summary>
    /// <param name="rootPath">Корневой путь для сканирования.</param>
    /// <param name="maxDepth">
    /// Максимальная глубина обхода поддиректорий.
    /// 0 = только rootPath, 1 = rootPath + прямые подпапки.
    /// </param>
    /// <param name="onFile">
    /// Callback, вызываемый для каждого файла, прошедшего фильтр.
    /// Может быть null если действие не требуется.
    /// </param>
    /// <param name="filter">
    /// Опциональный фильтр. Если null, обрабатываются все файлы.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">maxDepth &lt; 0</exception>
    /// <exception cref="DirectoryNotFoundException">rootPath не существует</exception>
    void Scan(
        string rootPath,
        int maxDepth,
        Action<FileInfo>? onFile = null,
        IFileFilter? filter = null);
}