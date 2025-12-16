using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Domain.Abstractions.Services;
using DiskAnalyzer.Domain.Extensions;
using DiskAnalyzer.Domain.Models;
using DiskAnalyzer.Domain.Models.Results;

namespace DiskAnalyzer.Domain.Services;

/// <summary>
/// Сервис для группировки файлов по заданному критерию с подсчётом метрик.
/// </summary>
/// <remarks>
/// Поддерживает множественные метрики (TotalSize, FilesCount и т.д.).
/// Каждая группа получает отдельные экземпляры IFilesMeasurement.
/// </remarks>
public class FilesGrouper(IFileSystemScanner scanner) : IFilesGrouper
{
    /// <summary>
    /// Выполняет группировку файлов с подсчётом метрик.
    /// </summary>
    /// <param name="measurements">
    /// Набор измерений для сбора. Для каждой группы создаются копии.
    /// </param>
    /// <param name="grouper">Стратегия определения ключа группы.</param>
    /// <returns>
    /// Результат с группами файлов, метриками и списками файлов в каждой группе.
    /// <see cref="GroupingAnalysisResult"/>
    /// </returns>
    public GroupingAnalysisResult GroupFiles(
        string path,
        int maxDepth,
        IEnumerable<IFilesMeasurement> measurements,
        IFileGrouper grouper,
        IFileFilter? filter = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);      
        ArgumentNullException.ThrowIfNull(grouper);            
        ArgumentNullException.ThrowIfNull(measurements);

        var measurementsList = measurements.ToList();

        var groups = new Dictionary<string, GroupData>();

        scanner.Scan(
            path,
            maxDepth,
            file =>
            {
                var key = grouper.GetKey(file);

                if (!groups.TryGetValue(key, out var group))
                {
                    group = new GroupData();
                    group.Measurements.AddRange(
                        measurements.Select(m => CreateMeasurementInstance(m))
                    );
                    groups[key] = group;
                }

                foreach (var measurement in group.Measurements)
                    measurement.OnFileAction(file);

                group.Files.Add(file);
            },
            filter);

        var fileGroups = new List<FilesGroup>();
        var metrics = new Dictionary<string, string>();

        foreach (var (key, group) in groups)
        {
            var groupMetrics = new Dictionary<string, long>();

            foreach (var measurement in group.Measurements)
            {
                groupMetrics.Add(measurement.MeasurementType, measurement.Result);

                var metricKey = $"{key}_{measurement.MeasurementType}";
                metrics.Add(metricKey, measurement.Result.ToString());
            }

            fileGroups.Add(new FilesGroup(
                key,
                groupMetrics,
                [.. group.Files.Select(f => new FileDetails(f.FullName, f.Length))]
            ));
        }

        metrics.Add("GrouperType", grouper.ToGrouperInfo().Type);

        return new GroupingAnalysisResult(
            path,
            filter?.ToFilterInfoList(),
            grouper.ToGrouperInfo().Type,
            metrics,
            fileGroups
        );
    }

    /// <summary>
    /// Асинхронная версия группировки.
    /// </summary>
    /// <remarks>
    /// Выполняется в отдельном потоке через 
    /// <see cref="Task.Run{TResult}(Func{TResult}, CancellationToken)"/>
    /// </remarks>
    public Task<GroupingAnalysisResult> GroupFilesAsync(
        string path,
        int maxDepth,
        IEnumerable<IFilesMeasurement> measurements,
        IFileGrouper grouper,
        IFileFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
            GroupFiles(path, maxDepth, measurements, grouper, filter),
            cancellationToken);
    }

    private static IFilesMeasurement CreateMeasurementInstance(IFilesMeasurement template)
    {
        return (IFilesMeasurement)Activator.CreateInstance(template.GetType())!;
    }

    private sealed class GroupData
    {
        public List<IFilesMeasurement> Measurements { get; } = [];
        public List<FileInfo> Files { get; } = [];
    }
}