using DiskAnalyzer.Domain.Abstractions;

namespace DiskAnalyzer.Domain.Services.FilesMeasurements;

public class FilesCountMeasurement : IFilesMeasurement
{
    public string MeasurementType => "Count";

    public long Result { get; private set; }

    public Action<FileInfo> OnFileAction => _ => Result++;
}
