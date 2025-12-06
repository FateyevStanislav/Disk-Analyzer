using DiskAnalyzer.Domain.Extensions;
using DiskAnalyzer.Domain.Services.FilesMeasurements;
using DiskAnalyzer.Infrastructure;
using DiskAnalyzer.Infrastructure.Filter;

namespace DiskAnalyzer.Domain.Services;

public class FilesMeasurer(DirectoryWalker walker)
{
    public AnalysisResult MeasureFiles(
        string path,
        int maxDepth,
        IEnumerable<IFilesMeasurement> measurements,
        IFileFilter? filter = null)
    {
        Action<FileInfo>? onFileAction = null;

        foreach (var act in measurements)
            onFileAction += act.OnFileAction;

        walker.Walk(path, maxDepth, onFileAction, filter);

        var result = new Dictionary<string, string>();

        foreach (var measurement in measurements)
            result.Add(measurement.MeasurementType, measurement.Result.ToString());

        return new AnalysisResult(path, "FilesMeasurement", result, filter.ToFilterInfoList());
    }

    public Task<AnalysisResult> MeasureFilesAsync(
        string path,
        int maxDepth,
        IEnumerable<IFilesMeasurement> measurements,
        IFileFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        return Task.Run(() => MeasureFiles(path, maxDepth, measurements, filter), cancellationToken);
    }
}