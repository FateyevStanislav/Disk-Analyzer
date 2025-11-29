using DiskAnalyzer.Domain.Records;
using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Measurements.FilesInDirectory;

public interface IDirectoryMeasurement
{
    DirectoryMeasurementRecord MeasureFilesInDirectory(
        string rootPath, 
        int maxDepth, 
        IFileFilter? filter = null);

    public async Task<DirectoryMeasurementRecord> MeasureFilesInDirectoryAsync(
        string rootPath,
        int maxDepth,
        IFileFilter? filter = null)
    {
        return await Task.Run(() => MeasureFilesInDirectory(rootPath, maxDepth, filter));
    }
}
