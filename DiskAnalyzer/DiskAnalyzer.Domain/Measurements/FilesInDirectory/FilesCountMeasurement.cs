using DiskAnalyzer.Domain.Metrics.Files;
using DiskAnalyzer.Domain.Records;
using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Measurements.FilesInDirectory;

public class FilesCountMeasurement : IDirectoryMeasurement
{
    public DirectoryMeasurementRecord MeasureFilesInDirectory(
        string rootPath,
        int maxDepth,
        IFileFilter? filter = null)
    {
        int count = 0;

        var walker = new DirectoryWalker();
        walker.Walk(
            rootPath,
            maxDepth,
            onFile: file => count++,
            filter: filter
        );

        var logs = walker.Logs?.Logs
            .Select(log => log.ToString())
            .ToList()
            .AsReadOnly();

        var metric = new FileCountMetric(count);
        return new DirectoryMeasurementRecord(
            Guid.NewGuid(),
            rootPath,
            logs,
            new[] { metric });
    }
}