using DiskAnalyzer.Library.Domain.Filters;

namespace DiskAnalyzer.Library.Domain.Groupers;

public interface IGrouper
{
    string Name { get; }
    IEnumerable<IGrouping<string, FileInfo>> Group(
        string rootPath, 
        int maxDepth, 
        IFileFilter? filter = null);
}
