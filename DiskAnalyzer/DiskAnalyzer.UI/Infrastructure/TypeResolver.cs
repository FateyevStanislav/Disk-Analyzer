using DiskAnalyzer.Library.Domain.Attributes;
using System.Reflection;

namespace DiskAnalyzer.UI.Infrastructure
{
    public class TypeResolver : ITypeResolver
    {
        public Type GetTypeByDisplayName(string name, Type targetInterface)
        {
            var type = Assembly
                .Load("DiskAnalyzer.Library")
                .GetTypes()
                .Where(x => x.GetInterfaces().Contains(targetInterface))
                .Where(x => x.GetDisplayName() == name)
                .FirstOrDefault();

            return type;
        }
    }
}
