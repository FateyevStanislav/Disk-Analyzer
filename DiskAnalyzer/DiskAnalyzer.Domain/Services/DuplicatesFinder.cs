using DiskAnalyzer.Domain.Extensions;
using DiskAnalyzer.Domain.Records;
using DiskAnalyzer.Domain.Records.DuplicateFind;
using DiskAnalyzer.Infrastructure;
using DiskAnalyzer.Infrastructure.Filter;

namespace DiskAnalyzer.Domain.Services;

public class DuplicatesFinder(DirectoryWalker walker)
{
    public DuplicatesSearchRecord FindDuplicates(
        string path,
        int maxDepth,
        IFileFilter? filter = null)
    {
        var filesBySize = CollectFilesBySize(path, maxDepth, filter);
        var duplicateGroups = FindDuplicateGroups(filesBySize);
        var totalWastedSpace = CalculateTotalWastedSpace(duplicateGroups);

        var sortedGroups = duplicateGroups
            .OrderByDescending(g => g.TotalWastedSpace)
            .ToList();

        return new DuplicatesSearchRecord(
            path,
            sortedGroups.Count,
            totalWastedSpace,
            sortedGroups,
            filter?.ToFilterInfoList());
    }

    public Task<DuplicatesSearchRecord> FindDuplicatesAsync(
        string path,
        int maxDepth,
        IFileFilter? filter = null,
        CancellationToken cancellationToken = default)
    {
        return Task.Run(() => FindDuplicates(path, maxDepth, filter), cancellationToken);
    }

    private Dictionary<long, List<FileInfo>> CollectFilesBySize(
        string path,
        int maxDepth,
        IFileFilter? filter)
    {
        var filesBySize = new Dictionary<long, List<FileInfo>>();

        walker.Walk(path, maxDepth, file =>
        {
            if (file.Length == 0) return;

            if (!filesBySize.ContainsKey(file.Length))
                filesBySize[file.Length] = new List<FileInfo>();

            filesBySize[file.Length].Add(file);
        }, filter);

        return filesBySize;
    }

    private static List<DuplicateGroup> FindDuplicateGroups(
        Dictionary<long, List<FileInfo>> filesBySize)
    {
        var duplicateGroups = new List<DuplicateGroup>();

        foreach (var (size, files) in filesBySize)
        {
            if (files.Count < 2) continue;

            var duplicates = FindDuplicatesInGroup(files, size);
            duplicateGroups.AddRange(duplicates);
        }

        return duplicateGroups;
    }

    private static IEnumerable<DuplicateGroup> FindDuplicatesInGroup(
        List<FileInfo> files,
        long fileSize)
    {
        var quickHashGroups = files
            .GroupByQuickHash()
            .Where(g => g.Count() > 1);

        foreach (var quickGroup in quickHashGroups)
        {
            var fullHashGroups = quickGroup
                .GroupByFullHash()
                .Where(g => g.Count() > 1);

            foreach (var duplicateGroup in fullHashGroups)
            {
                yield return CreateDuplicateGroup(duplicateGroup, fileSize);
            }
        }
    }

    private static DuplicateGroup CreateDuplicateGroup(
        IGrouping<string, FileInfo> duplicateGroup,
        long fileSize)
    {
        var fileList = duplicateGroup.ToList();
        var count = fileList.Count;
        var wastedSpace = fileSize * (count - 1);

        return new DuplicateGroup(
            FileHash: duplicateGroup.Key,
            FileSize: fileSize,
            FileCount: count,
            TotalWastedSpace: wastedSpace,
            Files: [.. fileList.Select(f => new FileDetails(f.FullName, f.Length))]); 
    }

    private static long CalculateTotalWastedSpace(List<DuplicateGroup> groups)
    {
        return groups.Sum(g => g.TotalWastedSpace);
    }
}