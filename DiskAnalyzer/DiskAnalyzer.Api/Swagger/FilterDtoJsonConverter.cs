using DiskAnalyzer.Api.Controllers.Filters;
using System.Text.Json;
using System.Text.Json.Serialization;

public class FilterDtoJsonConverter : JsonConverter<FilterDto>
{
    public override FilterDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        var root = jsonDoc.RootElement;

        if (!root.TryGetProperty("type", out var typeProp))
            throw new JsonException("Filter must contain 'type' field");

        var type = typeProp.GetString();

        return type switch
        {
            "AcessTime" => JsonSerializer.Deserialize<FilterAcessTimeDto>(root, options)!,
            "CreationTime" => JsonSerializer.Deserialize<FilterCreationTimeDto>(root, options)!,
            "Extension" => JsonSerializer.Deserialize<FilterExtensionDto>(root, options)!,
            "Size" => JsonSerializer.Deserialize<FilterSizeDto>(root, options)!,
            "WriteTime" => JsonSerializer.Deserialize<FilterWriteTimeDto>(root, options)!,

            _ => throw new JsonException($"Unknown filter type: {type}")
        };
    }

    public override void Write(Utf8JsonWriter writer, FilterDto value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
    }
}
