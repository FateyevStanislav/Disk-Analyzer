using DiskAnalyzer.Domain.Extensions;
using DiskAnalyzer.Domain.Records;
using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Services;

public class FilesGrouper(DirectoryWalker walker)
{
    public FilesGroupingRecord GroupFiles(
        string path,
        int maxDepth,
        IFileGrouper grouper,
        IFileFilter? filter = null)
    {
        var groups = new Dictionary<string, (long count, long size, List<FileDetails> files)>();

        walker.Walk(
            path,
            maxDepth,
            file =>
            {
                var key = grouper.GetKey(file);

                if (!groups.ContainsKey(key))
                    groups[key] = (0, 0, new List<FileDetails>());

                var (count, size, files) = groups[key];
                groups[key] = (
                    count + 1,
                    size + file.Length,
                    files
                );
                files.Add(new FileDetails(file.FullName, file.Length)); 
            },
            filter);

        var fileGroups = groups
            .Select(kvp => new FileGroup(
                Key: kvp.Key,
                FileCount: kvp.Value.count,
                TotalSize: kvp.Value.size,
                Files: kvp.Value.files))
            .OrderByDescending(g => g.TotalSize)
            .ToList();

        return new FilesGroupingRecord(
            path,
            grouper.ToGrouperInfo(),
            fileGroups,
            filter?.ToFilterInfoList());
    }

    public Task<FilesGroupingRecord> GroupFilesAsync(
        string path,
        int maxDepth,
        IFileGrouper grouper,
        IFileFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        return Task.Run(() => GroupFiles(path, maxDepth, grouper, filter), cancellationToken);
    }
}