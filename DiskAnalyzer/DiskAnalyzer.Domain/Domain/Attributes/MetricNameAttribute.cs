using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskAnalyzer.Library.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MetricNameAttribute : Attribute
    {
        public string Name { get; }
        public MetricNameAttribute(string name) => Name = name;
    }
}
