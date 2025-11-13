using DiskAnalyzer.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace disk_analyzer.Application
{
    public record RequestDto(string Path, int MaxDepth);

    internal class WeightingService
    {
        public static void InitService(WebApplication app)
        {
            app.MapPost("/weightings",
                (RequestDto dto) =>
                {
                    var id = StartWeighting(dto.Path, dto.MaxDepth);
                    return Results.Created($"/weightings/{id}", new { id });
                }
                );

            app.MapGet("/weightings/{id:guid}",
                (Guid id) =>
                {
                    try
                    {
                        var record = GetWeighting(id);
                        return Results.Ok(record);
                    }
                    catch (Exception ex)
                    {
                        return Results.NotFound(ex);
                    }
                }
                );
        }

        public static Guid StartWeighting(string path, int maxDepth)
        {
            var id = Guid.NewGuid();

            try
            {
                var count = FileWeigher.CountFiles(path, maxDepth);
                var newRecord = new WeightingRecord(id, path, maxDepth, count, null);
                WeightingRecordRepository.AddRecord(newRecord);
            }

            catch (Exception ex)
            {
                var newRecord = new WeightingRecord(id, path, maxDepth, 0, ex.Message);
                WeightingRecordRepository.AddRecord(newRecord);
            }

            return id;
        }

        public static WeightingRecord? GetWeighting(Guid guid) => WeightingRecordRepository.GetRecord(guid);
    }
}
