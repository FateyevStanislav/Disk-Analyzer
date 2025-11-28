namespace DiskAnalyzer.Api.Controllers.Filters
{
    public enum FilterType
    {
        AcessTime,
        CreationTime,
        Extension,
        Size,
        WriteTime
    }

    public abstract record FilterDto
    {
        public abstract FilterType Type { get; }
    }
}
