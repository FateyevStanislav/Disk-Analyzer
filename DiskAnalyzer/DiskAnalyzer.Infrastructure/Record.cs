namespace DiskAnalyzer.Infrastructure;

public abstract record Record(string Path, DateTime CreatedAt)
{
    protected Record(string path)
        : this(path, DateTime.UtcNow)
    {
    }
}
