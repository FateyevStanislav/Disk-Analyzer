using DiskAnalyzer.Library.Domain.Records;
using DiskAnalyzer.Library.Infrastructure.Filters;

namespace DiskAnalyzer.Library.Domain.Measurements.FilesInDirectory;

public interface IDirectoryMeasurement
{
    DirectoryMeasurementRecord MeasureFilesInDirectory(
        string rootPath, 
        int maxDepth, 
        IFileFilter? filter = null);
}
