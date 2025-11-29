using DiskAnalyzer.Domain.Records;
using DiskAnalyzer.Domain.Records.Groups;
using DiskAnalyzer.Infrastructure;
using Microsoft.Extensions.Logging;

namespace DiskAnalyzer.Domain.Measurements;

public class FilesMeasurement(ILogger<DirectoryWalker> walkerLogger, ILogger<FilesMeasurement> logger)
{
    public SimpleMeasurementRecord MeasureCount(string rootPath, int maxDepth, IFileFilter? filter = null)
    {
        logger.LogInformation("Подсчёт файлов: {RootPath}, глубина {MaxDepth}", rootPath, maxDepth);
        var filtersInfo = filter?.ToFilterInfoList();

        long count = 0;
        var walker = new DirectoryWalker(walkerLogger);
        walker.Walk(rootPath, maxDepth, _ => count++, filter);

        logger.LogInformation("Найдено {Count} файлов в {RootPath}", count, rootPath);

        return new SimpleMeasurementRecord(
            Guid.NewGuid(), rootPath, MeasurementType.FileCount, count, filtersInfo, DateTime.UtcNow);
    }

    public SimpleMeasurementRecord MeasureSize(string rootPath, int maxDepth, IFileFilter? filter = null)
    {
        logger.LogInformation("Подсчёт размера: {RootPath}, глубина {MaxDepth}", rootPath, maxDepth);
        var filtersInfo = filter?.ToFilterInfoList();

        long totalSize = 0;
        var walker = new DirectoryWalker(walkerLogger);
        walker.Walk(rootPath, maxDepth, f => totalSize += f.Length, filter);

        logger.LogInformation("Размер {TotalSize} байт в {RootPath}", totalSize, rootPath);

        return new SimpleMeasurementRecord(
            Guid.NewGuid(), rootPath, MeasurementType.FileSize, totalSize, filtersInfo, DateTime.UtcNow);
    }

    public CombinedGroupsMeasurementRecord MeasureCombined(
        string rootPath, 
        int maxDepth, 
        IFileFilter? filter = null)
    {
        logger.LogInformation("Комбо-измерение: {RootPath}, глубина {MaxDepth}", rootPath, maxDepth);
        var filtersInfo = filter?.ToFilterInfoList();

        long count = 0, totalSize = 0;
        var walker = new DirectoryWalker(walkerLogger);
        walker.Walk(rootPath, maxDepth, f =>
        {
            count++;
            totalSize += f.Length;
        }, filter);

        logger.LogInformation(
            "Комбо: {Count} файлов, {TotalSize} байт в {RootPath}", 
            count, totalSize, rootPath);

        return new CombinedGroupsMeasurementRecord(
            Guid.NewGuid(), rootPath, count, totalSize, filtersInfo, DateTime.UtcNow);
    }
}