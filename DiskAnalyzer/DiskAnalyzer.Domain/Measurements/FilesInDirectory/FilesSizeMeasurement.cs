using DiskAnalyzer.Domain.Metrics.Files;
using DiskAnalyzer.Domain.Records;
using DiskAnalyzer.Infrastructure;
using Microsoft.Extensions.Logging;

namespace DiskAnalyzer.Domain.Measurements.FilesInDirectory;

public class FilesSizeMeasurement(
    ILogger<DirectoryWalker> walkerLogger,
    ILogger<FilesSizeMeasurement> logger) : IDirectoryMeasurement
{
    public DirectoryMeasurementRecord MeasureFilesInDirectory(
        string rootPath,
        int maxDepth,
        IFileFilter? filter = null)
    {
        long totalSize = 0;

        var walker = new DirectoryWalker(walkerLogger);

        logger.LogInformation(
            "Начато измерение размера файлов {RootPath} максимальная глубина {MaxDepth}",
            rootPath, maxDepth);

        walker.Walk(
            rootPath,
            maxDepth,
            onFile: file => totalSize += file.Length,
            filter: filter);

        var metric = new FileSizeMetric(totalSize);

        logger.LogInformation(
            "Измерение окончено {RootPath}. Размер: {TotalSize} бит",
            rootPath, totalSize);

        return new DirectoryMeasurementRecord(
            Guid.NewGuid(),
            rootPath,
            metrics: [metric]);
    }
}