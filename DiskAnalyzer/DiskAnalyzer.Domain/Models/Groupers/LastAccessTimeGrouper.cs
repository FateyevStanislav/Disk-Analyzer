using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Domain.Attributes;

namespace DiskAnalyzer.Domain.Models.Groupers;

[GrouperType("LastAccessTime")]
public class LastAccessTimeGrouper : IFileGrouper
{
    public string GetKey(FileInfo file)
    {
        var span = DateTime.UtcNow - file.LastAccessTimeUtc;
        if (span.TotalDays > 365) return "Неиспользуемые";
        if (span.TotalDays > 30) return "Используемые";
        return "Наиболее актуальные";
    }
}
