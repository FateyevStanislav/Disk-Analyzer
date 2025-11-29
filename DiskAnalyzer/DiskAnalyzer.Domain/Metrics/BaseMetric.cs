using DiskAnalyzer.Domain.Metrics.Formatters;
using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.Domain.Metrics;

public abstract class BaseMetric : IMetric
{
    public abstract string Name { get; }
    protected abstract object RawValue { get; }
    private readonly IValueFormatter formatter;

    public BaseMetric(IValueFormatter formatter)
    {
        this.formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
    }

    public string Value => formatter.Format(RawValue);
}