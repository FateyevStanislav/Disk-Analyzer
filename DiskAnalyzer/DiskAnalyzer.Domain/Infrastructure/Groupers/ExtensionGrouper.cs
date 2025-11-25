using DiskAnalyzer.Library.Infrastructure.Filters;

namespace DiskAnalyzer.Library.Infrastructure.Groupers;

public class ExtensionGrouper : IGrouper
{
    public string Name => "Группировка по расширению";

    public IEnumerable<IGrouping<string, FileInfo>> Group(
        string rootPath, 
        int maxDepth, 
        IFileFilter? filter = null)
    {
        return FileGrouping.GroupFilesBy(
            rootPath, 
            maxDepth, 
            f => f.Extension.ToLowerInvariant(), 
            filter);
    }
}