using Bounteous.Core.Utilities;

namespace Bounteous.Core.Serialization;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class DateTimeRangeConverter : JsonConverter<DateTimeRange>
{
    public override void Write(Utf8JsonWriter writer, DateTimeRange value, JsonSerializerOptions options)
    {
        if (value == null) return;
        writer.WriteStringValue(JsonSerializer.Serialize(new DateTimeRangeDto(value)));
    }

    public override DateTimeRange Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null) return null;
        if (reader.TokenType != JsonTokenType.String) throw new JsonException();

        var json = reader.GetString();
        if (string.IsNullOrWhiteSpace(json) || IsEqual(json, "null")) return null;

        var fromJson = JsonSerializer.Deserialize<DateTimeRangeDto>(json);
        return new DateTimeRange(fromJson.Start, fromJson.End);
    }

    public override bool CanConvert(Type typeToConvert)
        => typeToConvert == typeof(DateTimeRange);

    private static bool IsEqual(string condition, string value)
        => string.Equals(condition, value, StringComparison.InvariantCultureIgnoreCase);
}

public class DateTimeRangeDto : RangeDto<DateTime>
{
    public DateTimeRangeDto()
    {
    }

    public DateTimeRangeDto(DateTimeRange range)
    {
        Start = range.Start;
        End = range.End;
    }
}

public class RangeDto<T>
{
    public T Start { get; set; }
    public T End { get; set; }
}