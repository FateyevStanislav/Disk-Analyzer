namespace DiskAnalyzer.Infrastructure;

public abstract record Group(string Key, IReadOnlyList<FileDetails> Files);