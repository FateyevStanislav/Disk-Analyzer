namespace DiskAnalyzer.Infrastructure;

public static class DirectoryInfoExtensions
{
    public static List<FileSystemInfo> GetFilesSortedby<TSelected>(
        this DirectoryInfo directory, 
        Func<FileSystemInfo, TSelected> selector, 
        bool reversed = false)
        where TSelected : IComparable
    {
        var files = directory.EnumerateFileSystemInfos().ToList();
        files.Sort((x, y) => selector(x).CompareTo(selector(y)) * (reversed ? -1 : 1));
        return files;
    }

    public static List<FileSystemInfo> GetFilesWhere(
        this DirectoryInfo directory,
        Func<FileSystemInfo, bool> predicate)
    {
        return directory.EnumerateFileSystemInfos().Where(f => predicate(f)).ToList();
    }

    public static IEnumerable<IGrouping<TKey, FileSystemInfo>> GetFilesGroupedBy<TKey>(
        this DirectoryInfo directory,
        Func<FileSystemInfo, TKey> keySelector)
    {
        return directory.EnumerateFileSystemInfos().GroupBy(f => keySelector(f));
    }
}
