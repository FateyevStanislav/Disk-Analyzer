using DiskAnalyzer.Infrastructure;
using Microsoft.Extensions.Logging;

namespace DiskAnalyzer.Domain.Groupers;

public class ExtensionGrouper(ILoggerFactory loggerFactory) : IFileGrouper
{
    public IEnumerable<IGrouping<string, FileInfo>> Group(
        string rootPath, 
        int maxDepth, 
        IFileFilter? filter = null)
    {
        return FileGrouping.GroupFilesBy(
            rootPath, 
            maxDepth, 
            f => f.Extension.ToLowerInvariant(), 
            filter,
            loggerFactory);
    }
}