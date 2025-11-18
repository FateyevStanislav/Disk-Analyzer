using DiskAnalyzer.Library.Infrastructure;

namespace DiskAnalyzer.Library.Domain
{
    public abstract class Metric : ValueType<Metric>
    {
        public abstract string Name { get; }
        public abstract object Value { get; }
    }
    public abstract class Metric<T> : Metric
    {
        public T TypedValue { get; protected set; }
        public override object Value => TypedValue;
        protected Metric(T value) => TypedValue = value;
        public override string ToString() => $"{Name}: {TypedValue}";
    }
}
