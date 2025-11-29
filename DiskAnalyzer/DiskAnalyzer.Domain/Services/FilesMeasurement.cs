using DiskAnalyzer.Domain.Extensions;
using DiskAnalyzer.Domain.Records;
using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Services;

public class FileAnalyzer(DirectoryWalker walker)
{
    public FilesMeasurementRecord MeasureFilesInDirectory(
        string path,
        int maxDepth,
        IFileFilter? filter = null)
    {
        long fileCount = 0;
        long totalSize = 0;

        walker.Walk(path, maxDepth, onFile: file =>
        {
            fileCount++;
            totalSize += file.Length;
        }, filter);

        return new FilesMeasurementRecord(
            path,
            fileCount,
            totalSize,
            filter?.ToFilterInfoList());
    }
}