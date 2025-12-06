using DiskAnalyzer.Domain.Attributes;
using DiskAnalyzer.Infrastructure.Grouper;

namespace DiskAnalyzer.Infrastructure.Groupers;

[GrouperType("Extension")]
public class ExtensionGrouper : IFileGrouper
{
    public string GetKey(FileInfo file) => file.Extension.ToLowerInvariant();
}