using DiskAnalyzer.Domain.Models.Results;

namespace DiskAnalyzer.Domain.Abstractions.Services;

public interface IFilesGrouper
{
    GroupingAnalysisResult GroupFiles(
        string path,
        int maxDepth,
        IEnumerable<IFilesMeasurement> measurements,
        IFileGrouper grouper,
        IFileFilter? filter = null);

    Task<GroupingAnalysisResult> GroupFilesAsync(
        string path,
        int maxDepth,
        IEnumerable<IFilesMeasurement> measurements,
        IFileGrouper grouper,
        IFileFilter? filter = null,
        CancellationToken cancellationToken = default);
}
