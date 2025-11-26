using DiskAnalyzer.Library.Infrastructure.Filters;

namespace DiskAnalyzer.Library.Infrastructure.Groupers;

public class LastAccessTimeGrouper : IFileGrouper
{
    public string Name => "Группировка по времени последнего доступа";

    public IEnumerable<IGrouping<string, FileInfo>> Group(
        string rootPath, 
        int maxDepth, 
        IFileFilter? filter = null)
    {
        return FileGrouping.GroupFilesBy(
            rootPath, 
            maxDepth, 
            f => {
                var span = DateTime.UtcNow - f.LastAccessTimeUtc;
                if (span.TotalDays > 365) return "Неиспользуемые";
                if (span.TotalDays > 30) return "Используемые";
                return "Наиболее актуальные";
            }, 
            filter);
    }
}
