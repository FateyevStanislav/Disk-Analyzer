using DiskAnalyzer.Library.Domain.Filters;
using DiskAnalyzer.Library.Domain.Metrics;

namespace DiskAnalyzer.Library.Domain;

public class FileWeigher
{
    public static WeightingRecord CountFiles(string rootPath, int maxDepth, IFileFilter filter = null)
    {
        int count = 0;
        var walker = new DirectoryWalker();
        walker.Walk(
            rootPath, maxDepth,
            onFile: file => count++,
            filter: filter
        );
        var logs = walker.Logger.Logs
            .Select(log => log.ToString())
            .ToList()
            .AsReadOnly();
        var metric = new FileCountMetricType(count);
        return new WeightingRecord(
            Guid.NewGuid(),
            rootPath,
            logs,
            new[] { metric });
    }

    public static WeightingRecord CalcTotalSize(string rootPath, int maxDepth, IFileFilter filter = null)
    {
        long totalSize = 0;
        var walker = new DirectoryWalker();
        walker.Walk(
            rootPath, maxDepth,
            onFile: file => totalSize += file.Length,
            filter: filter
        );
        var logs = walker.Logger.Logs
            .Select(log => log.ToString())
            .ToList()
            .AsReadOnly();
        var metric = new FileSizeMetricType(totalSize);
        return new WeightingRecord(
            Guid.NewGuid(),
            rootPath,
            logs,
            new[] { metric });
    }
}
