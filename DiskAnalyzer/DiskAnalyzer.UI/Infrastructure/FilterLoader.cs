using DiskAnalyzer.Library.Domain.Attributes;
using DiskAnalyzer.Library.Infrastructure.Filters;
using System.Reflection;

namespace DiskAnalyzer.UI.Infrastructure
{
    public class FilterLoader : IFilterLoader
    {
        public IEnumerable<string> GetAvailableFilters()
        {
            var metricTypes = Assembly
                .Load("DiskAnalyzer.Library")
                .GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(IFileFilter)))
                .Where(x => x.GetCustomAttributes(typeof(FilterNameAttribute), false).Length > 0)
                .Select(m => m.GetDisplayName());

            return metricTypes;        
        }
    }
}
