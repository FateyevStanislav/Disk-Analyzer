using DiskAnalyzer.Domain.Records.Grouping;

namespace DiskAnalyzer.Domain.Records.RecordStrategies.Grouping;

public class CombinedInfoGroupStrategy : IFilesGroupStrategy
{
    public Group CreateGroup(
        string key,
        long fileCount,
        long totalSize,
        IReadOnlyList<FileDetails> files) => new CombineInfoFileGroup(
            key, fileCount, totalSize, files);
}
