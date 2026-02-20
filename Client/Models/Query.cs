using System.Text.Json;
using System.Text.Json.Serialization;

namespace Client.Models;

public sealed class Query
{
    private Query(string value)
    {
        Value = value;
    }

    private Query(Dictionary<string, JsonElement> values)
    {
        Values = values;
    }

    public string? Value { get; }
    public Dictionary<string, JsonElement>? Values { get; }

    public static implicit operator Query(string value) => new(value);

    public static implicit operator Query(Dictionary<string, JsonElement> values) => new(values);
}

internal sealed class QueryConverter : JsonConverter<Query>
{
    public override Query Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.String =>
                reader.GetString() ?? string.Empty,

            JsonTokenType.StartObject =>
                JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(ref reader, options) ?? [],

            _ => throw new JsonException("Unsupported query format")
        };
    }

    public override void Write(Utf8JsonWriter writer, Query value, JsonSerializerOptions options)
    {
        if (value.Value != null)
        {
            writer.WriteStringValue(value.Value);
        }
        else if (value.Values != null)
        {
            JsonSerializer.Serialize(writer, value.Values, options);
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}