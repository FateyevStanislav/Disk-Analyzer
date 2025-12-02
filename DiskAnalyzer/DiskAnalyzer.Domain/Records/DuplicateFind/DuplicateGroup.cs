namespace DiskAnalyzer.Domain.Records.DuplicateFind;

public record DuplicateGroup(
    string FileHash,
    long FileSize,
    int FileCount,
    long TotalWastedSpace,
    IReadOnlyList<FileDetails> Files);