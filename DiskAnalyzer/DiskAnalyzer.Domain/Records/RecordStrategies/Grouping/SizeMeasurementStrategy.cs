using DiskAnalyzer.Domain.Records.Grouping;

namespace DiskAnalyzer.Domain.Records.RecordStrategies.Grouping;

public class SizeInfoGroupStrategy : IFilesGroupStrategy
{
    public Group CreateGroup(
        string key,
        long fileCount,
        long totalSize,
        IReadOnlyList<FileDetails> files) => new SizeInfoFileGroup(key, totalSize, files);
}