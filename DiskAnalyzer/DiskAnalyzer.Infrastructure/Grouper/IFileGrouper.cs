namespace DiskAnalyzer.Infrastructure.Grouper;

public interface IFileGrouper
{
    string GetKey(FileInfo file);
}
