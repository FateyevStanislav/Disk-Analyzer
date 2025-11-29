namespace DiskAnalyzer.Infrastructure;

public interface IFileFilter
{
    bool ShouldInclude(FileInfo file);
}