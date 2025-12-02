namespace DiskAnalyzer.Domain.Records.Grouping;

public record CountInfoFileGroup(
    string Key,
    long FileCount,
    IReadOnlyList<FileDetails> Files) : Group(Key, Files);
