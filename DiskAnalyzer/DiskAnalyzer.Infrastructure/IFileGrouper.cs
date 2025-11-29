namespace DiskAnalyzer.Infrastructure;

public interface IFileGrouper
{
    string GetKey(FileInfo file);
}
