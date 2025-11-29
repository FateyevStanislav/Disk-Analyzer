using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Groupers;

public class ExtensionGrouper : IFileGrouper
{
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