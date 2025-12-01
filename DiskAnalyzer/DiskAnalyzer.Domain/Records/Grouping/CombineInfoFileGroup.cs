namespace DiskAnalyzer.Domain.Records.Grouping;

public record CombineInfoFileGroup(
    string Key,
    long FileCount,
    long TotalSize,
    IReadOnlyList<FileDetails> Files) : Group(Key, Files);