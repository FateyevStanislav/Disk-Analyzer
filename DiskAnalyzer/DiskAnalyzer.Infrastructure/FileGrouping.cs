using Microsoft.Extensions.Logging;

namespace DiskAnalyzer.Infrastructure;

public static class FileGrouping
{
    public static IEnumerable<IGrouping<string, FileInfo>> GroupFilesBy(
        string rootPath,
        int maxDepth,
        Func<FileInfo, string> keySelector,
        IFileFilter? filter,
        ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger<DirectoryWalker>();
        var walker = new DirectoryWalker(logger);

        var files = new List<FileInfo>();

        walker.Walk(
            rootPath,
            maxDepth,
            onFile: files.Add,
            filter: filter);

        return files.GroupBy(keySelector);
    }
}