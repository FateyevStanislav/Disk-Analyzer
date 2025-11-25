using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskAnalyzer.Library.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FilterNameAttribute : Attribute
    {
        public string Name { get; }
        public FilterNameAttribute(string name) => Name = name;
    }
}
