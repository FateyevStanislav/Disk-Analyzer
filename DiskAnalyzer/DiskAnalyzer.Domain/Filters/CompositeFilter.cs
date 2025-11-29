using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Filters;

public class CompositeFilter : IFileFilter
{
    public List<IFileFilter> Filters { get; } = [];

    public void Add(IFileFilter filter) => Filters.Add(filter);

    public bool ShouldInclude(FileInfo file) =>
        Filters.All(f => f.ShouldInclude(file));
}