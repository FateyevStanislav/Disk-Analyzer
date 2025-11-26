using DiskAnalyzer.Library.Domain;
using DiskAnalyzer.Library.Domain.Attributes;
using DiskAnalyzer.Library.Domain.Metrics;
using System.Reflection;

namespace DiskAnalyzer.UI.Infrastructure
{
    public class MetricLoader : IMetricLoader
    {
        public IEnumerable<string> GetAvailableMetrics(Type targetInterface)
        {
            var metricTypes = Assembly
                .Load("DiskAnalyzer.Library")
                .GetTypes()
                .Where(x => x.GetInterfaces().Contains(targetInterface))
                .Select(m => m.GetDisplayName());

            return metricTypes;
        }
    }
}
