namespace DiskAnalyzer.Domain.Services.FilesMeasurements;

public interface IFilesMeasurement
{
    string MeasurementType { get; }

    long Result { get; }

    Action<FileInfo> OnFileAction { get; }
}
