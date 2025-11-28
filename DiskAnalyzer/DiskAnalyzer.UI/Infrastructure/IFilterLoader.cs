namespace DiskAnalyzer.UI.Infrastructure
{
    public interface IFilterLoader
    {
        public IEnumerable<string> GetAvailableFilters();
    }
}
