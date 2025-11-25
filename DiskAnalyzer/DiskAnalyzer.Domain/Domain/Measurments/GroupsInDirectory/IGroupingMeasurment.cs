using DiskAnalyzer.Library.Domain.Records;
using DiskAnalyzer.Library.Infrastructure.Filters;
using DiskAnalyzer.Library.Infrastructure.Groupers;

namespace DiskAnalyzer.Library.Domain.Measurments.GroupsInDirectory;

public interface IGroupingMeasurment
{
    IEnumerable<GroupingRecord> MeasureGroupsInDirectory(
        string rootPath, 
        int maxDepth,
        IGrouper grouper,
        IFileFilter? filter = null);
}