using DiskAnalyzer.Library.Domain.Filters;
using DiskAnalyzer.Library.Domain.Groupers;
using DiskAnalyzer.Library.Domain.Records;

namespace DiskAnalyzer.Library.Domain.Measurments.GroupsInDirectory;

public interface IGroupingMeasurment
{
    IEnumerable<GroupingRecord> MeasureGroupsInDirectory(
        string rootPath, 
        int maxDepth,
        IGrouper grouper,
        IFileFilter? filter = null);
}