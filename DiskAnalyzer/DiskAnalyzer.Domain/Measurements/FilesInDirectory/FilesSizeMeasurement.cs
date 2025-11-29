using DiskAnalyzer.Domain.Metrics.Files;
using DiskAnalyzer.Domain.Records;
using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Measurements.FilesInDirectory;

public class FilesSizeMeasurement : IDirectoryMeasurement
{
    public DirectoryMeasurementRecord MeasureFilesInDirectory(
        string rootPath,
        int maxDepth,
        IFileFilter? filter = null)
    {
        long totalSize = 0;

        var walker = new DirectoryWalker();
        walker.Walk(
            rootPath,
            maxDepth,
            onFile: file => totalSize += file.Length,
            filter: filter
        );

        var logs = walker.Logs?.Logs
            .Select(log => log.ToString())
            .ToList()
            .AsReadOnly();

        var metric = new FileSizeMetric(totalSize);
        return new DirectoryMeasurementRecord(
            Guid.NewGuid(),
            rootPath,
            logs,
            new[] { metric });
    }
}