using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Domain.Extensions;
using DiskAnalyzer.Domain.Models.Results;

namespace DiskAnalyzer.Domain.Services;

public class FilesMeasurer(IFileSystemScanner walker)
{
    public MeasurementAnalysisResult MeasureFiles(
        string path,
        int maxDepth,
        IEnumerable<IFilesMeasurement> measurements,
        IFileFilter? filter = null)
    {
        Action<FileInfo>? onFileAction = null;

        foreach (var act in measurements)
            onFileAction += act.OnFileAction;

        walker.Scan(path, maxDepth, onFileAction, filter);

        var result = new Dictionary<string, string>();

        foreach (var measurement in measurements)
            result.Add(measurement.MeasurementType, measurement.Result.ToString());

        return new MeasurementAnalysisResult(
            Guid.NewGuid(),
            DateTime.UtcNow,
            path,
            filter?.ToFilterInfoList(),
            result);
    }

    public Task<MeasurementAnalysisResult> MeasureFilesAsync(
        string path,
        int maxDepth,
        IEnumerable<IFilesMeasurement> measurements,
        IFileFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        return Task.Run(() => MeasureFiles(path, maxDepth, measurements, filter), cancellationToken);
    }
}