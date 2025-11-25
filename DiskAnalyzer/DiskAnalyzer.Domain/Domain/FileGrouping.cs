using DiskAnalyzer.Library.Domain.Filters;
using DiskAnalyzer.Library.Infrastructure.Logger;

namespace DiskAnalyzer.Library.Domain;

public static class FileGrouping
{
    public static IEnumerable<IGrouping<TKey, FileInfo>> GroupFilesBy<TKey>(
        string rootPath,
        int maxDepth,
        Func<FileInfo, TKey> groupBySelector,
        IFileFilter? filter = null,
        Logger? logger = null)
    {
        var files = new List<FileInfo>();

        var walker = new DirectoryWalker(logger);
        walker.Walk(
            rootPath,
            maxDepth,
            onFile: files.Add,
            filter: filter);

        return files.GroupBy(groupBySelector);
    }
}