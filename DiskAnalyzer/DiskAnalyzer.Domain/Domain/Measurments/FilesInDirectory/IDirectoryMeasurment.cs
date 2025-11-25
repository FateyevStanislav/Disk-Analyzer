using DiskAnalyzer.Library.Domain.Filters;
using DiskAnalyzer.Library.Domain.Records;

namespace DiskAnalyzer.Library.Domain.Measurments.FilesInDirectory;

public interface IDirectoryMeasurment
{
    DirectoryMeasurmentRecord MeasureFilesInDirectory(
        string rootPath, 
        int maxDepth, 
        IFileFilter? filter = null);
}
