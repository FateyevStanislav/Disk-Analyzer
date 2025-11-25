using DiskAnalyzer.Library.Domain.Filters;

namespace DiskAnalyzer.Library.Domain.Groupers;

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