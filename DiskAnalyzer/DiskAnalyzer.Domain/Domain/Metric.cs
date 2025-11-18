namespace DiskAnalyzer.Library.Domain
{
    public interface IMetrics<TValue>
    {
        public TValue Value { get; }
    }
}
