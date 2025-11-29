namespace DiskAnalyzer.Infrastructure;

public static class FileGrouping
{
    public static IEnumerable<IGrouping<TKey, FileInfo>> GroupFilesBy<TKey>(
        string rootPath,
        int maxDepth,
        Func<FileInfo, TKey> groupBySelector,
        IFileFilter? filter = null,
        Logger.Logger? logger = null)
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