using DiskAnalyzer.Library.Domain.Filters;
using DiskAnalyzer.Library.Domain.Metrics;
using DiskAnalyzer.Library.Domain.Records;

namespace DiskAnalyzer.Library.Domain.Measurments;

public class FileCountMeasurment : IMeasurment
{
    public MeasurmentRecord Measure(string rootPath, int maxDepth, IFileFilter? filter = null)
    {
        int count = 0;

        var walker = new DirectoryWalker();
        walker.Walk(
            rootPath, 
            maxDepth,
            onFile: file => count++,
            filter: filter
        );

        var logs = walker.Logger?.Logs
            .Select(log => log.ToString())
            .ToList()
            .AsReadOnly();

        var metric = new FileCountMetric(count);
        return new MeasurmentRecord(
            Guid.NewGuid(),
            rootPath,
            logs,
            new[] { metric });
    }
}
