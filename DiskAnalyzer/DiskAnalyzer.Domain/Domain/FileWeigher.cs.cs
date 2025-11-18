using DiskAnalyzer.Library.Domain.Filters;

namespace DiskAnalyzer.Library.Domain;

public class FileWeigher
{
    public static int CountFiles(string rootPath, int maxDepth, IFileFilter filter = null)
    {
        int count = 0;
        var walker = new DirectoryWalker();
        walker.Walk(
            rootPath, maxDepth,
            onFile: file => count++,
            filter: filter
        );
        return count;
    }

    public static long CalcTotalSize(string rootPath, int maxDepth, IFileFilter filter = null)
    {
        long totalSize = 0;
        var walker = new DirectoryWalker();
        walker.Walk(
            rootPath, maxDepth,
            onFile: file => totalSize += file.Length,
            filter: filter
        );
        return totalSize;
    }
}
