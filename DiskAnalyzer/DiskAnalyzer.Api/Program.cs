using DiskAnalyzer.Library.Domain;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/api/weightings", (RequestDto dto) =>
{
    var id = Guid.NewGuid();
    try
    {
        var count = FileWeigher.CountFiles(dto.Path, dto.MaxDepth);
        var record = new WeightingRecord(id, dto.Path, dto.MaxDepth, count, null);
        WeightingRecordRepository.AddRecord(record);
    }
    catch (Exception ex)
    {
        var record = new WeightingRecord(id, dto.Path, dto.MaxDepth, 0, ex.Message);
        WeightingRecordRepository.AddRecord(record);
    }
    return Results.Created($"/api/weightings/{id}", new { id });
});

app.MapGet("/api/weightings/{id:guid}", (Guid id) =>
{
    try
    {
        var record = WeightingRecordRepository.GetRecord(id);
        return Results.Ok(record);
    }
    catch
    {
        return Results.NotFound();
    }
});

app.Run();

public record RequestDto(string Path, int MaxDepth);
