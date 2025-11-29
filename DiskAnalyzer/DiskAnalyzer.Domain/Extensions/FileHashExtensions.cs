namespace DiskAnalyzer.Domain.Extensions;

public static class FileHashExtensions
{
    public static IEnumerable<IGrouping<string, FileInfo>> GroupByHash(
        this IEnumerable<FileInfo> files,
        Func<FileInfo, string> hashSelector)
    {
        return files.GroupBy(hashSelector);
    }

    public static IEnumerable<IGrouping<string, FileInfo>> GroupByQuickHash(
        this IEnumerable<FileInfo> files)
    {
        return files.GroupByHash(f => f.GetQuickHashString());
    }

    public static IEnumerable<IGrouping<string, FileInfo>> GroupByFullHash(
        this IEnumerable<FileInfo> files)
    {
        return files.GroupByHash(f => f.GetFileContentHashString());
    }
}