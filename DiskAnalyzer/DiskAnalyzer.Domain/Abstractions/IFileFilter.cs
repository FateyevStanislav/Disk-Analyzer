namespace DiskAnalyzer.Domain.Abstractions;

public interface IFileFilter
{
    bool ShouldInclude(FileInfo file);
}