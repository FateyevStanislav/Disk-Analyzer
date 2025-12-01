namespace DiskAnalyzer.Infrastructure.Filter;

public interface IFileFilter
{
    bool ShouldInclude(FileInfo file);
}