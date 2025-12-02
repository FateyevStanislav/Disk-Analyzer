using DiskAnalyzer.Infrastructure.Filter;
using Microsoft.Extensions.Logging;

namespace DiskAnalyzer.Infrastructure;

public class DirectoryWalker(ILogger<DirectoryWalker> logger)
{
    public delegate void OnFileAction(FileInfo file);

    public void Walk(
        string rootPath,
        int maxDepth,
        OnFileAction? onFile = null,
        IFileFilter? filter = null)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(maxDepth);

        if (!Directory.Exists(rootPath))
            throw new DirectoryNotFoundException(rootPath);

        logger.LogInformation("Начат обход {RootPath}, глубина {MaxDepth}", rootPath, maxDepth);

        var q = new Queue<(string path, int depth)>();
        q.Enqueue((rootPath, 0));

        while (q.Count > 0)
        {
            var (path, depth) = q.Dequeue();
            int fileCount = 0;

            try
            {
                foreach (var filePath in Directory.EnumerateFiles(path, "*", SearchOption.TopDirectoryOnly))
                {
                    var file = new FileInfo(filePath);
                    if (filter?.ShouldInclude(file) != false)
                    {
                        onFile?.Invoke(file);
                        fileCount++;
                    }
                }
                logger.LogDebug("Обработано {FileCount} файлов в {Path}", fileCount, path);
            }
            catch (UnauthorizedAccessException ex)
            {
                logger.LogWarning(ex, "Нет доступа к файлам в {Path}", path);
            }
            catch (IOException ex)
            {
                logger.LogError(ex, "I/O ошибка при чтении файлов в {Path}", path);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при обработке файлов в {Path}", path);
            }

            if (depth >= maxDepth) continue;

            try
            {
                var subdirs = Directory.EnumerateDirectories(path).ToList();
                foreach (var dir in subdirs)
                    q.Enqueue((dir, depth + 1));

                logger.LogDebug("Добавлено {SubdirCount} поддиректорий из {Path}", subdirs.Count, path);
            }
            catch (UnauthorizedAccessException ex)
            {
                logger.LogWarning(ex, "Нет доступа к поддиректориям {Path}", path);
            }
            catch (IOException ex)
            {
                logger.LogError(ex, "I/O ошибка при обходе поддиректорий {Path}", path);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при обходе поддиректорий {Path}", path);
            }
        }

        logger.LogInformation("Обход {RootPath} завершён", rootPath);
    }
}