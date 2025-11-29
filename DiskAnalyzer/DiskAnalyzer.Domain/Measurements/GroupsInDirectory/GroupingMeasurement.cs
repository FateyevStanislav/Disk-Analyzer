using DiskAnalyzer.Domain.Metrics.Groups;
using DiskAnalyzer.Domain.Records;
using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Measurements.GroupsInDirectory;

public class GroupMeasurement : IGroupingMeasurement
{
    public IEnumerable<GroupingRecord> MeasureGroupsInDirectory(
        string rootPath,
        int maxDepth,
        IFileGrouper grouper,
        IFileFilter? filter = null)
    {
        var groups = grouper.Group(rootPath, maxDepth, filter);

        foreach (var group in groups)
        {
            var key = group.Key ?? string.Empty;

            var fileCount = group.Count();
            var totalSize = group.Sum(f => f.Length);

            var metrics = new IMetric[]
            {
                new GroupCountMetric(fileCount, key),
                new GroupSizeMetric(totalSize, key)
            };

            yield return new GroupingRecord(Guid.NewGuid(), key, null, metrics);
        }
    }
}