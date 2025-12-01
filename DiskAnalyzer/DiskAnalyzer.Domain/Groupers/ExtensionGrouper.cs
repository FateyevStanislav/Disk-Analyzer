using DiskAnalyzer.Infrastructure.Grouper;

namespace DiskAnalyzer.Domain.Groupers;

[GrouperType("Extension")]
public class ExtensionGrouper : IFileGrouper
{
    public string GetKey(FileInfo file) => file.Extension.ToLowerInvariant();
}