using DiskAnalyzer.Library.Domain.Filters;
using DiskAnalyzer.Library.Infrastructure.Logger;

namespace DiskAnalyzer.Library.Domain;

public class DirectoryWalker
{
    public delegate void FileAction(FileInfo file);

    public Logger Logger { get; }

    public DirectoryWalker(Logger logger = null)
    {
        Logger = logger ?? new Logger();
    }

    public void Walk(
        string rootPath,
        int maxDepth,
        FileAction onFile = null,
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

            try
            {
                foreach (var filePath in Directory.EnumerateFiles(path, "*", SearchOption.TopDirectoryOnly))
                {
                    var file = new FileInfo(filePath);
                    if (filter == null || filter.ShouldInclude(file))
                    {
                        onFile?.Invoke(file);
                        Logger?.Add(
                            LogType.Success,
                            $"Файл {path} обработан");
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Logger?.Add(
                    LogType.Warning,
                    $"Нет доступа к файлам в каталоге {path}: {ex?.ToString()}");
            }
            catch (IOException ex)
            {
                Logger?.Add(
                    LogType.Error,
                    $"Ошибка ввода/вывода при чтении файлов каталога {path}: {ex?.ToString()}");
            }
            catch (Exception ex)
            {
                Logger?.Add(
                    LogType.Error,
                    $"Неожиданная ошибка при обработке файлов в каталоге {path}: {ex?.ToString()}");
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
                Logger?.Add(
                    LogType.Warning,
                    $"Нет доступа к вложенному каталогу {path}: {ex?.ToString()}");
            }
            catch (IOException ex)
            {
                Logger?.Add(
                    LogType.Error,
                    $"Ошибка ввода/вывода при обходе вложенных каталогов {path}: {ex?.ToString()}");
            }
            catch (Exception ex)
            {
                Logger?.Add(
                    LogType.Error,
                    $"Неожиданная ошибка при обработке подкаталога {path}: {ex?.ToString()}");
            }
        }
    }
}
