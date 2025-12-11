using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Domain.Attributes;

namespace DiskAnalyzer.Infrastructure.Filters;

[FilterType("Extension")]
public class ExtensionFilter(string extension) : IFileFilter
{
    [FilterInfo("Extension")]
    public string Extension { get; } = extension;

    public bool ShouldInclude(FileInfo file) => $".{Extension}" == file.Extension;
}