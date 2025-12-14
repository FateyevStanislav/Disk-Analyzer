namespace DiskAnalyzer.Domain.Abstractions;

/// <summary>
/// Определяет метрику, собираемую во время обхода файлов.
/// </summary>
public interface IFilesMeasurement
{
    /// <summary>
    /// Тип измерения (например, "TotalSize", "FilesCount").
    /// </summary>
    string MeasurementType { get; }

    /// <summary>
    /// Текущее значение метрики.
    /// </summary>
    long Result { get; }

    /// <summary>
    /// Action, вызываемый для каждого файла. Обновляет <see cref="Result"/>.
    /// </summary>
    Action<FileInfo> OnFileAction { get; }
}
