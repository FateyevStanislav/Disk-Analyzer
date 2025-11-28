namespace DiskAnalyzer.UI.Infrastructure
{
    public interface ITypeResolver
    {
        public Type GetTypeByDisplayName(string name, Type targetInterface);
    }
}
