namespace DiskAnalyzer.Domain.Records;

public record DuplicateGroup(
    string FileHash,
    long FileSize,
    int FileCount,
    long TotalWastedSpace,
    IReadOnlyList<string> FilePaths);