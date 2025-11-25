namespace DiskAnalyzer.Library.Infrastructure.Filters;

public class CompositeFilter : IFileFilter
{
    public string Name => "Composite";

    private readonly List<IFileFilter> filters = new();

    public void Add(IFileFilter filter) => filters.Add(filter);

    public bool ShouldInclude(FileInfo file) =>
        filters.All(f => f.ShouldInclude(file));
}