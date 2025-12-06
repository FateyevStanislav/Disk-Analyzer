namespace DiskAnalyzer.Domain.Abstractions;

public interface IFileSystemScanner
{
    void Scan(
        string rootPath,
        int maxDepth,
        Action<FileInfo>? onFile = null,
        IFileFilter? filter = null);
}