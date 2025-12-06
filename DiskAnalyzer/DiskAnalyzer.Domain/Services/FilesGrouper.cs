using DiskAnalyzer.Domain.Extensions;
using DiskAnalyzer.Domain.Services.FilesMeasurements;
using DiskAnalyzer.Infrastructure;
using DiskAnalyzer.Infrastructure.Filter;
using DiskAnalyzer.Infrastructure.Grouper;

namespace DiskAnalyzer.Domain.Services;

public class FilesGrouper(DirectoryWalker walker)
{
    public AnalysisResult GroupFiles(
        string path,
        int maxDepth,
        IEnumerable<IFilesMeasurement> measurements,
        IFileGrouper grouper,
        IFileFilter? filter = null)
    {
        var groups = new Dictionary<string, List<IFilesMeasurement>>();

        walker.Walk(
            path,
            maxDepth,
            file =>
            {
                var key = grouper.GetKey(file);

                if (!groups.ContainsKey(key))
                    groups[key] = [.. measurements.Select(m => CreateMeasurementInstance(m))];

                foreach (var measurement in groups[key])
                    measurement.OnFileAction(file);
            },
            filter);

        var result = new Dictionary<string, string>();

        foreach (var (key, groupMeasurements) in groups)
        {
            foreach (var measurement in groupMeasurements)
            {
                var metricKey = $"{key}_{measurement.MeasurementType}";
                result.Add(metricKey, measurement.Result.ToString());
            }
        }

        result.Add("GrouperType", grouper.ToGrouperInfo().ToString());

        return new AnalysisResult(
            path,
            "FilesGrouping",
            result,
            filter?.ToFilterInfoList());
    }

    private static IFilesMeasurement CreateMeasurementInstance(IFilesMeasurement template)
    {
        return (IFilesMeasurement)Activator.CreateInstance(template.GetType())!;
    }

    public Task<AnalysisResult> GroupFilesAsync(
        string path,
        int maxDepth,
        IEnumerable<IFilesMeasurement> measurements,
        IFileGrouper grouper,
        IFileFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
            GroupFiles(path, maxDepth, measurements, grouper, filter),
            cancellationToken);
    }
}