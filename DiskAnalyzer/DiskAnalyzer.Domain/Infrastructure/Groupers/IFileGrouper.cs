using DiskAnalyzer.Library.Infrastructure.Filters;

namespace DiskAnalyzer.Library.Infrastructure.Groupers;

public interface IFileGrouper
{
    string Name { get; }
    IEnumerable<IGrouping<string, FileInfo>> Group(
        string rootPath, 
        int maxDepth, 
        IFileFilter? filter = null);
}
