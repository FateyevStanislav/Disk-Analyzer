namespace DiskAnalyzer.Library.Infrastructure;

public static class DirectoryInfoExtensions
{
    public static IReadOnlyList<FileSystemInfo> GetFilesSortedBy<TSelected>(
        this DirectoryInfo directory,
        Func<FileSystemInfo, TSelected> selector,
        bool reversed = false)
        where TSelected : IComparable
    {
        var files = directory.EnumerateFileSystemInfos().ToList();
        files.Sort((x, y) => selector(x).CompareTo(selector(y)) * (reversed ? -1 : 1));
        return files;
    }

    public static IEnumerable<FileSystemInfo> GetFilesWhere(
        this DirectoryInfo directory,
        Predicate<FileSystemInfo> predicate)
    {
        return directory.EnumerateFileSystemInfos().Where(f => predicate(f));
    }

    public static IEnumerable<IGrouping<TKey, FileSystemInfo>> GetFilesGroupedBy<TKey>(
        this DirectoryInfo directory,
        Func<FileSystemInfo, TKey> keySelector)
    {
        return directory.EnumerateFileSystemInfos().GroupBy(f => keySelector(f));
    }
}