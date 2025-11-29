using DiskAnalyzer.Domain.Records;
using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Measurements.FilesInDirectory;

public interface IDirectoryMeasurement
{
    DirectoryMeasurementRecord MeasureFilesInDirectory(
        string rootPath, 
        int maxDepth, 
        IFileFilter? filter = null);
}
