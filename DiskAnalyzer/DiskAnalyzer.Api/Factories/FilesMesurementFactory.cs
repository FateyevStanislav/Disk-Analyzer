using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Domain.Services.FilesMeasurements;

namespace DiskAnalyzer.Api.Factories;

public enum FilesMeasurementType
{
    Count,
    Size
}

public static class FilesMesurementFactory
{
    public static IEnumerable<IFilesMeasurement> Create(IEnumerable<FilesMeasurementType> measurements)
    {
        var result = new List<IFilesMeasurement>();


        if (measurements != null)
        {
            foreach (var m in measurements)
            {
                switch (m)
                {
                    case FilesMeasurementType.Count:
                        result.Add(new FilesCountMeasurement());
                        break;

                    case FilesMeasurementType.Size:
                        result.Add(new TotalSizeMeasurement());
                        break;

                    default:
                        throw new Exception($"Unknown mesurement type {m}");
                }
            }
        }

        return result;
    }
}
