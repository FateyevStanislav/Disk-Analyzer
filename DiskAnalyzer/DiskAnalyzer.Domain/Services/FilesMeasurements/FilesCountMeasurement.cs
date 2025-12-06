
namespace DiskAnalyzer.Domain.Services.FilesMeasurements;

public class FilesCountMeasurement : IFilesMeasurement
{
    public string MeasurementType => "FilesCount";

    public long Result { get; private set; }

    public Action<FileInfo> OnFileAction => _ => Result++;
}
