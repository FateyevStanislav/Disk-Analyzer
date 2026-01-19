using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Domain.Attributes;

namespace DiskAnalyzer.Domain.Models.Groupers;

[GrouperType("Extension")]
public class ExtensionGrouper : IFileGrouper
{
    public string GetKey(FileInfo file) => file.Extension.ToLowerInvariant();
}