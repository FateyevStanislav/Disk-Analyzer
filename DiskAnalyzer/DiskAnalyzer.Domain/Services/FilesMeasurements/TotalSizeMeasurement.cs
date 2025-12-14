using DiskAnalyzer.Domain.Abstractions;

namespace DiskAnalyzer.Domain.Services.FilesMeasurements;

public class TotalSizeMeasurement : IFilesMeasurement
{
    public string MeasurementType => "Size";

    public long Result { get; private set; }

    public Action<FileInfo> OnFileAction => file => Result += file.Length;
}
