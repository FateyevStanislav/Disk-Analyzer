using DiskAnalyzer.Library.Domain.Metrics.Files;
using DiskAnalyzer.Library.Domain.Records;
using DiskAnalyzer.Library.Infrastructure;
using DiskAnalyzer.Library.Infrastructure.Filters;

namespace DiskAnalyzer.Library.Domain.Measurments.FilesInDirectory;

public class FilesCountMeasurment : IDirectoryMeasurment
{
    public DirectoryMeasurmentRecord MeasureFilesInDirectory(
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
        return new DirectoryMeasurmentRecord(
            Guid.NewGuid(),
            rootPath,
            logs,
            new[] { metric });
    }
}