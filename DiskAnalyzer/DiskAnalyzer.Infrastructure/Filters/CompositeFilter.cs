using DiskAnalyzer.Domain.Abstractions;

namespace DiskAnalyzer.Infrastructure.Filters;

public class CompositeFilter : ICompositeFilter
{
    private readonly List<IFileFilter> filters = [];

    public IReadOnlyList<IFileFilter> Filters => filters.AsReadOnly();

    public void Add(IFileFilter filter)
    {
        ArgumentNullException.ThrowIfNull(filter, nameof(filter));

        filters.Add(filter);
    }

    public bool ShouldInclude(FileInfo file) =>
        filters.All(f => f.ShouldInclude(file));
}