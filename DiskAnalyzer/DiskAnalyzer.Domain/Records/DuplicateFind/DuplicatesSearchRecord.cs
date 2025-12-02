using DiskAnalyzer.Infrastructure;
using DiskAnalyzer.Infrastructure.Filter;

namespace DiskAnalyzer.Domain.Records.DuplicateFind;

public record DuplicatesSearchRecord(
    string Path,
    int DuplicateGroupsCount,
    long TotalWastedSpace,
    IReadOnlyList<DuplicateGroup> DuplicateGroups,
    IReadOnlyCollection<FilterInfo>? Filters) : Record(Path);