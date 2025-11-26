using DiskAnalyzer.Library.Domain;
using DiskAnalyzer.Library.Domain.Attributes;
using System.Reflection;

namespace DiskAnalyzer.UI.Infrastructure
{
    public class MetricLoader : IMetricLoader
    {
        public IEnumerable<string> GetAvailableMetrics()
        {
            var metricTypes = Assembly
                .Load("DiskAnalyzer.Library")
                .GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(IMetric)))
                .Select(m => m.GetDisplayName());

            return metricTypes;
        }
    }
}
