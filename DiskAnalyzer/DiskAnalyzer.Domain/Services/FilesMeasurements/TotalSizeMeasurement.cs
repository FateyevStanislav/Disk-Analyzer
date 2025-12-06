
namespace DiskAnalyzer.Domain.Services.FilesMeasurements;

public class TotalSizeMeasurement : IFilesMeasurement
{
    public string MeasurementType => "TotalSize";

    public long Result { get; private set; }

    public Action<FileInfo> OnFileAction => file => Result += file.Length;
}
