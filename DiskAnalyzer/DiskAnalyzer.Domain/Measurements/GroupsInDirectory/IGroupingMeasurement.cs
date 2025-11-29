using DiskAnalyzer.Domain.Records;
using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Measurements.GroupsInDirectory;

public interface IGroupingMeasurement
{
    IEnumerable<GroupingRecord> MeasureGroupsInDirectory(
        string rootPath,
        int maxDepth,
        IFileGrouper grouper,
        IFileFilter? filter = null);
}