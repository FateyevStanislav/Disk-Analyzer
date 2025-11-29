using DiskAnalyzer.Domain.Extensions;
using DiskAnalyzer.Domain.Records;
using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Services;

public class FilesMeasurement(DirectoryWalker walker)
{
    public FilesMeasurementRecord MeasureFiles(
        string path,
        int maxDepth,
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

        return new FilesMeasurementRecord(
            path,
            fileCount,
            totalSize,
            filter?.ToFilterInfoList());
    }

    public Task<FilesMeasurementRecord> MeasureFilesAsync(
        string path,
        int maxDepth,
        IFileFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        return Task.Run(() => MeasureFiles(path, maxDepth, filter), cancellationToken);
    }
}