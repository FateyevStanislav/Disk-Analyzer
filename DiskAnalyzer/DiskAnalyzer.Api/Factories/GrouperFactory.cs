using DiskAnalyzer.Domain.Abstractions;
using DiskAnalyzer.Domain.Models.Groupers;

namespace DiskAnalyzer.Api.Factories;

public enum FilesGroupingType
{
    Extension,
    LastAcessTime,
    SizeBucket
}

internal static class GrouperFactory
{
    public static IFileGrouper? Create(FilesGroupingType groupingType)
    {
        switch (groupingType)
        {
            case FilesGroupingType.Extension:
                return new ExtensionGrouper();

            case FilesGroupingType.LastAcessTime:
                return new LastAccessTimeGrouper();

            case FilesGroupingType.SizeBucket:
                return new SizeBucketGrouper();

            default:
                throw new Exception("Uncorrect grouper type");
        }
    }
}
