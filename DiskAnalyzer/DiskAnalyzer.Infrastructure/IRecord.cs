namespace DiskAnalyzer.Infrastructure;

public interface IRecord
{
    Guid Id { get; }
    string Path { get; init; }
    DateTime CreatedAt { get; init; }
}