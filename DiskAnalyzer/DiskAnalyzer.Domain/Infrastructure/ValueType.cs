using System.Reflection;

namespace DiskAnalyzer.Library.Infrastructure;

public class ValueType<T>
{
    private static readonly PropertyInfo[] properties
        = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);

    protected bool ValueTypeEquals(T other)
    {
        if (ReferenceEquals(this, other))
            return true;

        if (other == null || other.GetType() != typeof(T))
            return false;

        foreach (var prop in properties)
        {
            var value1 = prop.GetValue(this);
            var value2 = prop.GetValue(other);
            if (!Equals(value1, value2))
                return false;
        }
        return true;
    }

    public override bool Equals(object obj)
    {
        return obj is T typed && ValueTypeEquals(typed);
    }

    public bool Equals(T obj) => ValueTypeEquals(obj);

    public override int GetHashCode()
    {
        var hash = new HashCode();
        foreach (var prop in properties)
        {
            hash.Add(prop.GetValue(this));
        }
        return hash.ToHashCode();
    }

    public override string ToString()
    {
        var typeName = typeof(T).Name;
        var props = string.Join("; ", properties
            .OrderBy(p => p.Name)
            .Select(p => $"{p.Name}: {p.GetValue(this) ?? ""}"));
        return $"{typeName}({props})";
    }
}