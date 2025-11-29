using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Groupers;

[GrouperType("SizeBucket")]
public class SizeBucketGrouper : IFileGrouper
{
    public string GetKey(FileInfo file)
    {
        var size = file.Length;
        if (size < 1L << 20) return "<1 MB";
        if (size < 10L << 20) return "1-10 MB";
        if (size < 100L << 20) return "10-100 MB";
        return ">=100 MB";
    }
}
