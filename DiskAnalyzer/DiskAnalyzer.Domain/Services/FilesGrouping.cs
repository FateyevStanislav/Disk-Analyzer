using DiskAnalyzer.Domain.Extensions;
using DiskAnalyzer.Domain.Records;
using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Services;

public class FileGroupingAnalyzer(DirectoryWalker walker)
{
    public FilesGroupingRecord GroupFiles(
        string path,
        int maxDepth,
        IFileGrouper grouper,
        IFileFilter? filter = null)
    {
        var groups = new Dictionary<string, (long count, long size, List<string> paths)>();

        walker.Walk(
            path, 
            maxDepth, 
            file =>
            {
                var key = grouper.GetKey(file);

                if (!groups.ContainsKey(key))
                    groups[key] = (0, 0, new List<string>());

                var (count, size, paths) = groups[key];
                groups[key] = (
                    count + 1,
                    size + file.Length,
                    paths
                );
                paths.Add(file.FullName);

            }, 
            filter);

        var fileGroups = groups
            .Select(kvp => new FileGroup(
                Key: kvp.Key,
                FileCount: kvp.Value.count,
                TotalSize: kvp.Value.size,
                FilePaths: kvp.Value.paths))
            .OrderByDescending(g => g.TotalSize)
            .ToList();

        return new FilesGroupingRecord(
            path,
            grouper.ToGrouperInfo(),
            fileGroups,
            filter?.ToFilterInfoList());
    }
}