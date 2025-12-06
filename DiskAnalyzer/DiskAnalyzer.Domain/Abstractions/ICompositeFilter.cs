namespace DiskAnalyzer.Domain.Abstractions;

public interface ICompositeFilter : IFileFilter
{
    IReadOnlyList<IFileFilter> Filters { get; }
}