using DiskAnalyzer.Domain.Extensions;
using DiskAnalyzer.Domain.Records.RecordStrategies.Measurement;
using DiskAnalyzer.Infrastructure;
using DiskAnalyzer.Infrastructure.Filter;

namespace DiskAnalyzer.Domain.Services;

public class FilesMeasurer(DirectoryWalker walker)
{
    public Record MeasureFiles(
        string path,
        int maxDepth,
        IFilesMeasurementStrategy strategy,
        IFileFilter? filter = null)
    {
        long fileCount = 0;
        long totalSize = 0;

        walker.Walk(
            path,
            maxDepth,
            file =>
            {
                fileCount++;
                totalSize += file.Length;
            },
            filter);

        return strategy.CreateRecord(
            path,
            fileCount,
            totalSize,
            filter?.ToFilterInfoList());
    }

    public Task<Record> MeasureFilesAsync(
        string path,
        int maxDepth,
        IFilesMeasurementStrategy strategy,
        IFileFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        return Task.Run(() => MeasureFiles(path, maxDepth, strategy, filter), cancellationToken);
    }
}