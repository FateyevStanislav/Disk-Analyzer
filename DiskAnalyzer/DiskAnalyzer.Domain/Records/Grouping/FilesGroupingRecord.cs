using DiskAnalyzer.Infrastructure;
using DiskAnalyzer.Infrastructure.Filter;
using DiskAnalyzer.Infrastructure.Grouper;

namespace DiskAnalyzer.Domain.Records.Grouping;

public record FilesGroupingRecord(
    string Path,
    GrouperInfo Grouper,
    IReadOnlyList<Group> Groups,
    IReadOnlyCollection<FilterInfo>? Filters) : Record(Path);
