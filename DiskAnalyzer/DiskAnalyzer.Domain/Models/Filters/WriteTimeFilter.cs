using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Domain.Attributes;

namespace DiskAnalyzer.Domain.Models.Filters;

[FilterType("WriteTime")]
public class WriteTimeFilter : IFileFilter
{
    [FilterInfo("MinDate")]
    public DateTime MinDateUtc { get; }

    [FilterInfo("MaxDate")]
    public DateTime MaxDateUtc { get; }

    public WriteTimeFilter(DateTime minDateUtc, DateTime maxDateUtc)
    {
        MinDateUtc = minDateUtc;
        MaxDateUtc = maxDateUtc;
    }

    public bool ShouldInclude(FileInfo file)
        => file.LastWriteTimeUtc <= MaxDateUtc && file.LastWriteTimeUtc >= MinDateUtc;
}