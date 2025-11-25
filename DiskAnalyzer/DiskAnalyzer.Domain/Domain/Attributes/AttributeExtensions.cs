using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DiskAnalyzer.Library.Domain.Attributes
{
    public static class AttributeExtensions
    {
        public static string GetDisplayName(this Type type)
        {
            var metricAttr = type.GetCustomAttribute<MetricNameAttribute>();
            if (metricAttr != null) return metricAttr.Name;

            var filterAttr = type.GetCustomAttribute<FilterNameAttribute>();
            if (filterAttr != null) return filterAttr.Name;

            return type.Name;
        }
    }
}
