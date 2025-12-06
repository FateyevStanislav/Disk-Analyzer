namespace DiskAnalyzer.Domain.Abstractions;

public interface IFilesMeasurement
{
    string MeasurementType { get; }

    long Result { get; }

    Action<FileInfo> OnFileAction { get; }
}
