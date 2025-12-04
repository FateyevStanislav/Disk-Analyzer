namespace DiskAnalyzer.Infrastructure;

public abstract record Record(Guid Id, string Path, DateTime CreatedAt)
{
    protected Record(string path)
        : this(Guid.NewGuid(), path, DateTime.UtcNow) { }
}