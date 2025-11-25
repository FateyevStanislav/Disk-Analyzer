namespace DiskAnalyzer.Library.Domain
{
    public interface IMetric
    {
        string Name { get; }
        string Value { get; }
    }
}
