using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DiskAnalyzer.Domain.Metrics.Groups;
using DiskAnalyzer.Domain.Records;
using DiskAnalyzer.Infrastructure;
using Microsoft.Extensions.Logging;

namespace DiskAnalyzer.Domain.Measurements.GroupsInDirectory;

public class GroupMeasurement(ILogger<GroupMeasurement> logger) : IGroupingMeasurement
{
    public IEnumerable<GroupingRecord> MeasureGroupsInDirectory(
        string rootPath,
        int maxDepth,
        IFileGrouper grouper,
        IFileFilter? filter = null)
    {
        logger.LogInformation(
            "Начато измерение групп {RootPath} максимальная глубина {MaxDepth}",
            rootPath, maxDepth);

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

            yield return new GroupingRecord(
                Guid.NewGuid(),
                key,
                logs: Array.Empty<string>(),
                metrics: metrics);
        }

        logger.LogInformation(
            "Измерение окончено {RootPath}",
            rootPath);
    }
}