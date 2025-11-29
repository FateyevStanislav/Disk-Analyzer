using DiskAnalyzer.Domain.Metrics.Files;
using DiskAnalyzer.Domain.Records;
using DiskAnalyzer.Infrastructure;
using Microsoft.Extensions.Logging;

namespace DiskAnalyzer.Domain.Measurements.FilesInDirectory;

public class FilesCountMeasurement(
    ILogger<DirectoryWalker> walkerLogger,
    ILogger<FilesSizeMeasurement> logger) : IDirectoryMeasurement
{
    public DirectoryMeasurementRecord MeasureFilesInDirectory(
        string rootPath,
        int maxDepth,
        IFileFilter? filter = null)
    {
        long count = 0;

        var walker = new DirectoryWalker(walkerLogger);

        logger.LogInformation(
            "Начато измерение количества файлов {RootPath} максимальная глубина {MaxDepth}",
            rootPath, maxDepth);

        walker.Walk(
            rootPath,
            maxDepth,
            onFile: file => count++,
            filter: filter);

        var metric = new FileSizeMetric(count);

        logger.LogInformation(
            "Измерение окончено {RootPath}. Количество: {Count}",
            rootPath, count);

        return new DirectoryMeasurementRecord(
            Guid.NewGuid(),
            rootPath,
            logs: Array.Empty<string>(),
            metrics: new[] { metric });
    }
}