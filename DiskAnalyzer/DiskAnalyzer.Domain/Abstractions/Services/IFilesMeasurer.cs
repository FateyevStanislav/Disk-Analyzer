using DiskAnalyzer.Domain.Models.Results;

namespace DiskAnalyzer.Domain.Abstractions.Services;

public interface IFilesMeasurer
{
    MeasurementAnalysisResult MeasureFiles(
        string path,
        int maxDepth,
        IEnumerable<IFilesMeasurement> measurements,
        IFileFilter? filter = null);

    Task<MeasurementAnalysisResult> MeasureFilesAsync(
        string path,
        int maxDepth,
        IEnumerable<IFilesMeasurement> measurements,
        IFileFilter? filter = null,
        CancellationToken cancellationToken = default);
}
