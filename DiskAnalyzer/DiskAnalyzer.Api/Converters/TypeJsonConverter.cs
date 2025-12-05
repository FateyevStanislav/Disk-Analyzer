using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiskAnalyzer.Api.Converters;

public class TypeJsonConverter : JsonConverter<Type>
{
    public override Type Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var typeName = reader.GetString();
        return Type.GetType(typeName!)
            ?? throw new JsonException($"Unknown type {typeName}");
    }

    public override void Write(Utf8JsonWriter writer, Type value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.FullName);
    }
}
