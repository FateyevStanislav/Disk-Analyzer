using DiskAnalyzer.Library.Domain.Metrics.Files;
using DiskAnalyzer.Library.Domain.Records;
using DiskAnalyzer.Library.Infrastructure;
using DiskAnalyzer.Library.Infrastructure.Filters;

namespace DiskAnalyzer.Library.Domain.Measurements.FilesInDirectory;

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