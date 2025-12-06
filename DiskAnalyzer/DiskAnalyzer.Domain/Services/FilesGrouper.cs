using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Domain.Extensions;
using DiskAnalyzer.Domain.Models;
using DiskAnalyzer.Domain.Models.Results;
using DiskAnalyzer.Infrastructure.Grouper;

namespace DiskAnalyzer.Domain.Services;

public class FilesGrouper(IFileSystemScanner walker)
{
    public AnalysisResult GroupFiles(
        string path,
        int maxDepth,
        IEnumerable<IFilesMeasurement> measurements,
        IFileGrouper grouper,
        IFileFilter? filter = null)
    {
        var groups = new Dictionary<string, (List<IFilesMeasurement> measurements, List<FileInfo> files)>();

        walker.Scan(
            path,
            maxDepth,
            file =>
            {
                var key = grouper.GetKey(file);

                if (!groups.ContainsKey(key))
                {
                    groups[key] = (
                        [.. measurements.Select(m => CreateMeasurementInstance(m))],
                        []
                    );
                }

                foreach (var measurement in groups[key].measurements)
                    measurement.OnFileAction(file);

                groups[key].files.Add(file);
            },
            filter);

        var fileGroups = new List<FileGroup>();
        var metrics = new Dictionary<string, string>();

        foreach (var (key, (groupMeasurements, files)) in groups)
        {
            var totalSize = groupMeasurements
                .FirstOrDefault(m => m.MeasurementType == "TotalSize")?.Result ?? 0;
            var filesCount = groupMeasurements
                .FirstOrDefault(m => m.MeasurementType == "FilesCount")?.Result ?? 0;

            fileGroups.Add(new FileGroup(
                key,
                totalSize,
                (int)filesCount,
                files.Select(f => new FileDetails(f.FullName, f.Length)).ToList()
            ));

            metrics.Add($"{key}_TotalSize", totalSize.ToString());
            metrics.Add($"{key}_FilesCount", filesCount.ToString());
        }

        metrics.Add("GrouperType", grouper.ToGrouperInfo().Type);

        return new GroupingAnalysisResult(
            Guid.NewGuid(),
            DateTime.UtcNow,
            path,
            filter?.ToFilterInfoList(),
            grouper.ToGrouperInfo().Type,
            metrics,
            fileGroups
        );
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