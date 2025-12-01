using DiskAnalyzer.Domain.Records.Grouping;

namespace DiskAnalyzer.Domain.Records.RecordStrategies.Grouping;

public class CountInfoGroupStrategy : IFilesGroupStrategy
{
    public Group CreateGroup(
        string key,
        long fileCount,
        long totalSize,
        IReadOnlyList<FileDetails> files) => new CountInfoFileGroup(key, fileCount, files);
}
