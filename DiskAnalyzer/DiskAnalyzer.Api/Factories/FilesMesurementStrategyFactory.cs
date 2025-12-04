using DiskAnalyzer.Domain.Records.RecordStrategies.Measurement;

namespace DiskAnalyzer.Api.Factories;

public enum FilesMeasurementStrategyType
{
    Combined,
    Count,
    Size
}

public static class FilesMesurementStrategyFactory
{
    public static IFilesMeasurementStrategy Create(FilesMeasurementStrategyType strategy)
    {
        switch (strategy)
        {
            case FilesMeasurementStrategyType.Combined:
                return new CombinedMeasurementStrategy();

            case FilesMeasurementStrategyType.Count:
                return new CountMeasurementStrategy();

            case FilesMeasurementStrategyType.Size:
                return new SizeMeasurementStrategy();

            default:
                throw new Exception("Unknown strategy type");
        }
    }
}
