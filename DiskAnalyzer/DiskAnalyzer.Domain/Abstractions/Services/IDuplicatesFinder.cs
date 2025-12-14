using DiskAnalyzer.Domain.Models.Results;

namespace DiskAnalyzer.Domain.Abstractions.Services;

public interface IDuplicatesFinder
{
    DuplicateAnalysisResult FindDuplicates(
        string path,
        int maxDepth,
        IFileFilter? filter = null);

    Task<DuplicateAnalysisResult> FindDuplicatesAsync(
        string path,
        int maxDepth,
        IFileFilter? filter = null,
        CancellationToken cancellationToken = default);
}
