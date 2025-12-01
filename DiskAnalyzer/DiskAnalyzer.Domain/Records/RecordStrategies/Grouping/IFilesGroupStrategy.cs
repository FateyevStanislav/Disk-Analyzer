using DiskAnalyzer.Domain.Records.Grouping;

namespace DiskAnalyzer.Domain.Records.RecordStrategies.Grouping;

public interface IFilesGroupStrategy
{
    Group CreateGroup(string key, long fileCount, long totalSize, IReadOnlyList<FileDetails> files);
}