namespace DiskAnalyzer.Library.Infrastructure.Filters;

[FilterName("Выбор по расширению")]
public class ExtensionFilter : IFileFilter
{
    public string Name => "Выбор по расширению";

    private readonly string extension;

    public ExtensionFilter(string extension)
    {
        this.extension = extension;
    }

    public bool ShouldInclude(FileInfo file) => extension == file.Extension;
}