using DiskAnalyzer.Library.Domain.Filters;
using Microsoft.Extensions.Logging; 

namespace DiskAnalyzer.Library.Domain;

public class DirectoryWalker
{
    public delegate void FileAction(FileInfo file);
    public delegate void DirectoryAction(DirectoryInfo dir);

    private readonly ILogger logger;

    public DirectoryWalker(ILogger logger = null)
    {
        this.logger = logger;
    }

    public void Walk(string rootPath, int maxDepth,
        FileAction onFile = null,
        DirectoryAction onDirectory = null,
        IFileFilter filter = null)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(maxDepth);

        if (!Directory.Exists(rootPath))
            throw new DirectoryNotFoundException(rootPath);

        var q = new Queue<(string path, int depth)>();
        q.Enqueue((rootPath, 0));

        while (q.Count > 0)
        {
            var (path, depth) = q.Dequeue();

            onDirectory?.Invoke(new DirectoryInfo(path));

            try
            {
                foreach (var filePath in Directory.EnumerateFiles(path, "*", SearchOption.TopDirectoryOnly))
                {
                    var file = new FileInfo(filePath);
                    if (filter == null || filter.ShouldInclude(file))
                        onFile?.Invoke(file);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                logger?.LogWarning(ex, $"Нет доступа к файлам в каталоге {path}");
            }
            catch (IOException ex)
            {
                logger?.LogError(ex, $"Ошибка ввода/вывода при чтении файлов каталога {path}");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, $"Неожиданная ошибка при обработке файлов в каталоге {path}");
            }

            if (depth >= maxDepth) continue;

            try
            {
                foreach (var dir in Directory.EnumerateDirectories(path))
                {
                    q.Enqueue((dir, depth + 1));
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                logger?.LogWarning(ex, $"Нет доступа к вложенному каталогу {path}");
            }
            catch (IOException ex)
            {
                logger?.LogError(ex, $"Ошибка ввода/вывода при обходе вложенных каталогов {path}");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, $"Неожиданная ошибка при обработке подкаталога {path}");
            }
        }
    }
}
