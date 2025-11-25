using DiskAnalyzer.Library.Infrastructure.Filters;

namespace DiskAnalyzer.Library.Infrastructure.Groupers;

public class SizeBucketGrouper : IGrouper
{
    public string Name => "Группировка по размеру";

    public IEnumerable<IGrouping<string, FileInfo>> Group(
        string rootPath,
        int maxDepth,
        IFileFilter? filter = null)
    {
        return FileGrouping.GroupFilesBy(
            rootPath,
            maxDepth,
            f =>
            {
                var size = f.Length;
                if (size < 1L << 20) return "<1 MB";
                if (size < 10L << 20) return "1–10 MB";
                if (size < 100L << 20) return "10–100 MB";
                return "≥100 MB";
            },
            filter);
    }
}
