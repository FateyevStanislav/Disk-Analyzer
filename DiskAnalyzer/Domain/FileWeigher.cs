namespace DiskAnalyzer.Domain;

public class FileWeigher
{
    public static int CountFiles(string rootPath, int maxDepth, Predicate<FileInfo> predicate = null)
    {
        if (maxDepth < 0)
            throw new ArgumentOutOfRangeException(nameof(maxDepth), "Глубина не может быть меньше нуля");
        if (!Directory.Exists(rootPath))
            throw new DirectoryNotFoundException(rootPath);

        int count = 0;
        var q = new Queue<(string path, int depth)>();
        q.Enqueue((rootPath, 0));
        while (q.Count > 0)
        {
            var (path, depth) = q.Dequeue();
            try
            {
                foreach (var filePath in Directory.EnumerateFiles(path, "*", SearchOption.TopDirectoryOnly))
                {
                    if (predicate == null || predicate(new FileInfo(filePath)))
                        count++;
                }
            }
            catch (UnauthorizedAccessException) { continue; }
            catch (IOException) { continue; }

            if (depth >= maxDepth) continue;

            try
            {
                foreach (var dir in Directory.EnumerateDirectories(path))
                    q.Enqueue((dir, depth + 1));
            }
            catch (UnauthorizedAccessException) { /* skip */ }
            catch (IOException) { /* skip */ }
        }

        return count;
    }
}
