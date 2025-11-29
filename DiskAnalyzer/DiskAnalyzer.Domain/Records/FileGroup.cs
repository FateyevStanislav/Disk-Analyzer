namespace DiskAnalyzer.Domain.Records;

public record FileGroup(
    string Key,
    long FileCount,
    long TotalSize,
    IReadOnlyList<FileDetails> Files);