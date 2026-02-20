using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Client.Models;

internal static class Converter
{
    public static readonly JsonSerializerOptions Settings = new(JsonSerializerDefaults.General)
    {
        Converters =
        {
            new DateOnlyConverter(),
            new TimeOnlyConverter(),
            IsoDateTimeOffsetConverter.Singleton
        },
    };
}

internal class BooleanConverter : JsonConverter<bool>
{
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.True => true,
            JsonTokenType.False => false,

            JsonTokenType.Number => ReadNumber(ref reader),

            JsonTokenType.String => ReadString(ref reader),

            _ => throw new JsonException($"Unsupported token type {reader.TokenType} for boolean conversion.")
        };
    }

    private static bool ReadNumber(ref Utf8JsonReader reader)
    {
        if (reader.TryGetInt64(out var l))
            return l != 0;

        throw new JsonException("Invalid numeric value for boolean.");
    }

    private static bool ReadString(ref Utf8JsonReader reader)
    {
        var str = reader.GetString();

        if (string.IsNullOrWhiteSpace(str))
            return false;

        if (bool.TryParse(str, out var b))
            return b;

        if (long.TryParse(str, out var l))
            return l != 0;

        throw new JsonException($"Invalid string value '{str}' for boolean.");
    }

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
    {
        writer.WriteBooleanValue(value);
    }
}

internal class DateOnlyConverter(string? serializationFormat) : JsonConverter<DateOnly>
{
    private readonly string serializationFormat = serializationFormat ?? "yyyy-MM-dd";

    public DateOnlyConverter() : this(null)
    {
    }

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return DateOnly.Parse(value!);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString(serializationFormat));
}

internal class LongStringConverter : JsonConverter<long>
{
    public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return long.TryParse(value, out long l) ? l : throw new JsonException("Cannot unmarshal type long");
    }

    public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.ToString(), options);
    }
}

internal class TimeOnlyConverter(string? serializationFormat) : JsonConverter<TimeOnly>
{
    private readonly string serializationFormat = serializationFormat ?? "HH:mm:ss.fff";

    public TimeOnlyConverter() : this(null)
    {
    }

    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return TimeOnly.Parse(value!);
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString(serializationFormat));
}

internal class IsoDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
{
    public static readonly IsoDateTimeOffsetConverter Singleton = new();
    private const string DefaultDateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

    private CultureInfo? _culture;
    private string? _dateTimeFormat;

    public CultureInfo Culture
    {
        get => _culture ?? CultureInfo.CurrentCulture;
        set => _culture = value;
    }

    public string? DateTimeFormat
    {
        get => _dateTimeFormat ?? string.Empty;
        set => _dateTimeFormat = (string.IsNullOrEmpty(value)) ? null : value;
    }

    public DateTimeStyles DateTimeStyles { get; set; } = DateTimeStyles.RoundtripKind;

    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? dateText = reader.GetString();

        if (!string.IsNullOrEmpty(dateText))
        {
            return !string.IsNullOrEmpty(_dateTimeFormat)
                ? DateTimeOffset.ParseExact(dateText, _dateTimeFormat, Culture, DateTimeStyles)
                : DateTimeOffset.Parse(dateText, Culture, DateTimeStyles);
        }
        else
        {
            return default;
        }
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        string text;

        if ((DateTimeStyles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.AdjustToUniversal ||
            (DateTimeStyles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.AssumeUniversal)
        {
            value = value.ToUniversalTime();
        }

        text = value.ToString(_dateTimeFormat ?? DefaultDateTimeFormat, Culture);

        writer.WriteStringValue(text);
    }
}