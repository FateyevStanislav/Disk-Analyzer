namespace DiskAnalyzer.Domain.Models;

public sealed record FileGroup(
    string Key,
    long TotalSize,
    int FilesCount,
    IReadOnlyList<FileDetails> Files);