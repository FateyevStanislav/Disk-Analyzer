using DiskAnalyzer.Library.Domain.Attributes;

namespace DiskAnalyzer.Library.Infrastructure.Filters;
public interface IFileFilter
{
    string Name { get; }

    bool ShouldInclude(FileInfo file);
}