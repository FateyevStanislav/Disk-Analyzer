namespace DiskAnalyzer.Library.Infrastructure.Logger;

public class Log
{
    public LogType LogType { get; }
    public string Message { get; }

    public Log(LogType logType, string message)
    {
        LogType = logType;
        Message = message;
    }

    public override string ToString()
    {
        return $"{LogType.ToString().ToUpper()}: {Message}";
    }
}
