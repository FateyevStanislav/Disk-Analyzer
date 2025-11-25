using DiskAnalyzer.Library.Domain.Filters;
using DiskAnalyzer.Library.Domain.Metrics.Files;
using DiskAnalyzer.Library.Domain.Records;

namespace DiskAnalyzer.Library.Domain.Measurments.FilesInDirectory;

public class FilesSizeMeasurment : IDirectoryMeasurment
{
    public DirectoryMeasurmentRecord MeasureFilesInDirectory(
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

        var logs = walker.Logger?.Logs
            .Select(log => log.ToString())
            .ToList()
            .AsReadOnly();

        var metric = new FileSizeMetric(totalSize);
        return new DirectoryMeasurmentRecord(
            Guid.NewGuid(),
            rootPath,
            logs,
            new[] { metric });
    }
}