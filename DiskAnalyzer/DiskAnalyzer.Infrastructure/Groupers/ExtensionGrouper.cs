using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Domain.Attributes;

namespace DiskAnalyzer.Infrastructure.Groupers;

[GrouperType("Extension")]
public class ExtensionGrouper : IFileGrouper
{
    public string GetKey(FileInfo file) => file.Extension.ToLowerInvariant();
}