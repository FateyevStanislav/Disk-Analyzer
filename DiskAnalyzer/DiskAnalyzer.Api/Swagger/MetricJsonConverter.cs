using System.Text.Json;
using System.Text.Json.Serialization;
using DiskAnalyzer.Library.Domain.Metrics;
using DiskAnalyzer.Library.Domain.Metrics.Files;

public class MetricJsonConverter : JsonConverter<IMetric>
{
    public override IMetric Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        var root = jsonDoc.RootElement;

        if (!root.TryGetProperty("name", out var nameProp) &&
            !root.TryGetProperty("Name", out nameProp))
        {
            throw new JsonException("Metric must contain 'name' field");
        }

        var name = nameProp.GetString();

        string valueString = "";
        if (root.TryGetProperty("value", out var valueProp) ||
            root.TryGetProperty("Value", out valueProp))
        {
            valueString = valueProp.GetString() ?? "";
        }

        return name switch
        {
            "FileCount" => CreateFileCountMetric(valueString),
            "FileSize" => CreateFileSizeMetric(valueString),
            _ => throw new JsonException($"Unknown metric type: {name}")
        };
    }

    private FileCountMetric CreateFileCountMetric(string valueString)
    {
        if (int.TryParse(valueString.Split(' ')[0], out int count))
        {
            return new FileCountMetric(count);
        }
        return new FileCountMetric(0);
    }

    private FileSizeMetric CreateFileSizeMetric(string valueString)
    {
        return new FileSizeMetric(0);
    }

    public override void Write(Utf8JsonWriter writer, IMetric value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}