using DiskAnalyzer.Library.Domain.Filters;
using DiskAnalyzer.Library.Domain.Records;

namespace DiskAnalyzer.Library.Domain.Measurments;

public interface IMeasurment
{
    MeasurmentRecord Measure(string rootPath, int maxDepth, IFileFilter? filter = null);
}
