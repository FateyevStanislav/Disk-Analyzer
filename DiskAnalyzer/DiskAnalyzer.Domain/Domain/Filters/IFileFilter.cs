namespace DiskAnalyzer.Library.Domain.Filters;

public interface IFileFilter
{
    string Name { get; }

    bool ShouldInclude(FileInfo file);
}
