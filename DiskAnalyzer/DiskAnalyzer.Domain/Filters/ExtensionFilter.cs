using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Filters;

public class ExtensionFilter(string extension) : IFileFilter
{
    public bool ShouldInclude(FileInfo file) => extension == file.Extension;
}