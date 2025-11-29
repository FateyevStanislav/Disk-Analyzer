using DiskAnalyzer.Domain.Filters;
using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Records;

public record DuplicatesSearchRecord(
    string Path,
    int DuplicateGroupsCount,
    long TotalWastedSpace,
    IReadOnlyList<DuplicateGroup> DuplicateGroups,
    IReadOnlyCollection<FilterInfo>? Filters) : Record(Path);