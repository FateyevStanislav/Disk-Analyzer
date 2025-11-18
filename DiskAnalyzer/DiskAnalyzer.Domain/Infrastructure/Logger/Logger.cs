namespace DiskAnalyzer.Library.Infrastructure.Logger;

public class Logger
{
    private readonly List<Log> logs = new();

    public IReadOnlyList<Log> Logs => logs.AsReadOnly();

    public void Add(LogType logType, string message)
    {
        logs.Add(new Log(logType, message));
    }
}
