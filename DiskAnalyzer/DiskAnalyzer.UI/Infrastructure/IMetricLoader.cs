namespace DiskAnalyzer.UI.Infrastructure
{
    public interface IMetricLoader
    {
        public IEnumerable<string> GetAvailableMetrics(Type targetInterface);
    }
}
