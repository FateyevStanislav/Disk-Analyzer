using DiskAnalyzer.Infrastructure.Filter;

namespace DiskAnalyzer.Domain.Filters;

public class CompositeFilter : IFileFilter
{
    private readonly List<IFileFilter> filters = [];

    public IReadOnlyCollection<IFileFilter> Filters => filters.AsReadOnly();

    public void Add(IFileFilter filter) => filters.Add(filter);

    public bool ShouldInclude(FileInfo file) =>
        filters.All(f => f.ShouldInclude(file));
}