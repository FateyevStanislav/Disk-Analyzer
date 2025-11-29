namespace DiskAnalyzer.Infrastructure;

public interface IFileGrouper
{
    IEnumerable<IGrouping<string, FileInfo>> Group(
        string rootPath,
        int maxDepth,
        IFileFilter? filter = null);
}
