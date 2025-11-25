using DiskAnalyzer.Library.Domain.Records;
using DiskAnalyzer.Library.Infrastructure.Filters;

namespace DiskAnalyzer.Library.Domain.Measurements.FilesInDirectory;

public interface IDirectoryMeasurement
{
    DirectoryMeasurmentRecord MeasureFilesInDirectory(
        string rootPath, 
        int maxDepth, 
        IFileFilter? filter = null);
}
