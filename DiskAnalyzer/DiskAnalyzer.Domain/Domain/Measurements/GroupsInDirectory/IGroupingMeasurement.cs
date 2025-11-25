using DiskAnalyzer.Library.Domain.Records;
using DiskAnalyzer.Library.Infrastructure.Filters;
using DiskAnalyzer.Library.Infrastructure.Groupers;

namespace DiskAnalyzer.Library.Domain.Measurements.GroupsInDirectory;

public interface IGroupingMeasurement
{
    IEnumerable<GroupingRecord> MeasureGroupsInDirectory(
        string rootPath, 
        int maxDepth,
        IGrouper grouper,
        IFileFilter? filter = null);
}