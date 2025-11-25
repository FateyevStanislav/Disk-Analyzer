using DiskAnalyzer.Library.Domain.Records;
using DiskAnalyzer.Library.Infrastructure.Filters;

namespace DiskAnalyzer.Library.Domain.Measurments.FilesInDirectory;

public interface IDirectoryMeasurment
{
    DirectoryMeasurmentRecord MeasureFilesInDirectory(
        string rootPath, 
        int maxDepth, 
        IFileFilter? filter = null);
}
