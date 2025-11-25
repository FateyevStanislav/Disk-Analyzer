using DiskAnalyzer.Library.Domain.Metrics;
using DiskAnalyzer.Library.Domain.Metrics.Groups;
using DiskAnalyzer.Library.Domain.Records;
using DiskAnalyzer.Library.Infrastructure.Filters;
using DiskAnalyzer.Library.Infrastructure.Groupers;

namespace DiskAnalyzer.Library.Domain.Measurments.GroupsInDirectory;

public class GroupMeasurement : IGroupingMeasurment
{
    public IEnumerable<GroupingRecord> MeasureGroupsInDirectory(
        string rootPath,
        int maxDepth,
        IGrouper grouper,
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