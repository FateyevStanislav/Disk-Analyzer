using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Domain.Attributes;

namespace DiskAnalyzer.Infrastructure.Filters;

[FilterType("Extension")]
public class ExtensionFilter : IFileFilter
{
    [FilterInfo("Extension")]
    public string Extension { get; }

    public ExtensionFilter(string extension)
    {
        Extension = extension.StartsWith('.')
            ? extension
            : $".{extension}";
    }

    public bool ShouldInclude(FileInfo file) =>
        file.Extension.Equals(Extension, StringComparison.OrdinalIgnoreCase);
}