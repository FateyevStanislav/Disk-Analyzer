using DiskAnalyzer.Library.Domain.Filters;

namespace DiskAnalyzer.Library.Domain.Groupers;

public class LastAccessTimeGrouper : IGrouper
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
