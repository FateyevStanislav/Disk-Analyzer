using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Domain.Abstractions.Services;
using DiskAnalyzer.Domain.Extensions;
using DiskAnalyzer.Domain.Models.Results;

namespace DiskAnalyzer.Domain.Services;

/// <summary>
/// Сервис для сбора общих метрик файловой системы без группировки.
/// </summary>
/// <remarks>
/// Более производительный чем FilesGrouper, когда группировка не требуется.
/// Все измерения работают на общих экземплярах IFilesMeasurement.
/// </remarks>
public class FilesMeasurer(IFileSystemScanner scanner) : IFilesMeasurer
{
    /// <summary>
    /// Собирает метрики по всем файлам в указанном пути.
    /// </summary>
    /// <param name="measurements">
    /// Набор измерений. Используются одни и те же экземпляры для всех файлов.
    /// </param>
    /// <returns>
    /// Результат с вычисленными метриками.
    /// <see cref="MeasurementAnalysisResult"/>
    /// </returns>
    public MeasurementAnalysisResult MeasureFiles(
        string path,
        int maxDepth,
        IEnumerable<IFilesMeasurement> measurements,
        IFileFilter? filter = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);      
        ArgumentNullException.ThrowIfNull(measurements);

        var measurementsList = measurements.ToList();
        if (measurementsList.Count == 0)
            throw new ArgumentException("Нужен минимум 1 тип измерения", nameof(measurements));

        Action<FileInfo>? onFileAction = null;

        foreach (var act in measurements)
            onFileAction += act.OnFileAction;

        scanner.Scan(path, maxDepth, onFileAction, filter);

        var result = new Dictionary<string, string>();

        foreach (var measurement in measurements)
            result.Add(measurement.MeasurementType, measurement.Result.ToString());

        return new MeasurementAnalysisResult(
            path,
            filter?.ToFilterInfoList(),
            result);
    }

    /// <summary>
    /// Асинхронная версия измерения файлов.
    /// </summary>
    /// <remarks>
    /// Выполняется в отдельном потоке через 
    /// <see cref="Task.Run{TResult}(Func{TResult}, CancellationToken)"/>
    /// </remarks>
    public Task<MeasurementAnalysisResult> MeasureFilesAsync(
        string path,
        int maxDepth,
        IEnumerable<IFilesMeasurement> measurements,
        IFileFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        return Task.Run(() => MeasureFiles(path, maxDepth, measurements, filter), cancellationToken);
    }
}