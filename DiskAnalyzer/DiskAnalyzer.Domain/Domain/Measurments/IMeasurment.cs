using DiskAnalyzer.Library.Domain.Filters;

namespace DiskAnalyzer.Library.Domain.Measurments;

public interface IMeasurment
{
    MeasurmentRecord Measure(string rootPath, int maxDepth, IFileFilter filter = null);
}
