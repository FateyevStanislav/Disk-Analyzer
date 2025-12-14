using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Domain.Abstractions.Services;
using DiskAnalyzer.Domain.Extensions;
using DiskAnalyzer.Domain.Models;
using DiskAnalyzer.Domain.Models.Results;

namespace DiskAnalyzer.Domain.Services;

/// <summary>
/// Сервис для поиска дублирующихся файлов на основе содержимого.
/// </summary>
/// <remarks>
/// Алгоритм:
/// 1. Группировка по размеру (быстрая предфильтрация)
/// 2. Quick hash первых 8KB (оптимизация для больших файлов)
/// 3. Full SHA256 hash для финальной проверки
/// 
/// Производительность: O(n log n) + I/O время на хеширование.
/// </remarks>
public class DuplicatesFinder(IFileSystemScanner scanner) : IDuplicatesFinder
{
    /// <summary>
    /// Выполняет синхронный поиск дубликатов.
    /// </summary>
    /// <param name="path">Корневой путь для поиска.</param>
    /// <param name="maxDepth">Глубина обхода поддиректорий.</param>
    /// <param name="filter">Опциональный фильтр файлов.</param>
    /// <returns>
    /// Результат анализа с найденными группами дубликатов 
    /// <see cref="DuplicateAnalysisResult"/>
    /// </returns>
    public DuplicateAnalysisResult FindDuplicates(
        string path,
        int maxDepth,
        IFileFilter? filter = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);

        var filesBySize = CollectFilesBySize(path, maxDepth, filter);
        var duplicateGroups = FindDuplicateGroups(filesBySize);
        var totalWastedSpace = CalculateTotalWastedSpace(duplicateGroups);
        var oldestOriginal = FindOldestFile(duplicateGroups);

        return new DuplicateAnalysisResult(
            path,
            filter?.ToFilterInfoList(),
            totalWastedSpace.ToString(),
            oldestOriginal,
            duplicateGroups);
    }

    /// <summary>
    /// Асинхронная версия поиска дубликатов.
    /// </summary>
    /// <remarks>
    /// Выполняется в отдельном потоке через 
    /// <see cref="Task.Run{TResult}(Func{TResult}, CancellationToken)"/>
    /// </remarks>
    public Task<DuplicateAnalysisResult> FindDuplicatesAsync(
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

        scanner.Scan(path, maxDepth, file =>
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
            duplicateGroup.Key,
            fileSize,
            count,
            wastedSpace,
            [.. fileList.Select(f => new FileDetails(f.FullName, f.Length))]);
    }

    private static long CalculateTotalWastedSpace(List<DuplicateGroup> groups)
    {
        return groups.Sum(g => g.TotalWastedSpace);
    }

    private static string FindOldestFile(List<DuplicateGroup> groups)
    {
        if (groups.Count == 0) return "N/A";

        var allFiles = groups
            .SelectMany(g => g.Files)
            .Select(f => new FileInfo(f.Path))
            .OrderBy(f => f.LastWriteTime)
            .ThenBy(f => f.CreationTime)
            .FirstOrDefault();

        return allFiles?.FullName ?? "N/A";
    }
}