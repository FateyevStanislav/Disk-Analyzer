using DiskAnalyzer.Domain.Filters;
using DiskAnalyzer.Domain.Groupers;
using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Records;

public record FilesGroupingRecord(
    string Path,
    GrouperInfo Grouper,
    IReadOnlyList<FileGroup> Groups,
    IReadOnlyCollection<FilterInfo>? Filters) : Record(Path);
