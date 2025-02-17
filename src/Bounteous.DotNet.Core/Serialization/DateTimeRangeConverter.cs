using System;
using Bounteous.DotNet.Core.Extensions;
using Bounteous.DotNet.Core.Utilities;
using Newtonsoft.Json;

namespace Bounteous.DotNet.Core.Serialization;

public class DateTimeRangeConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (!(value is DateTimeRange range)) return;
        writer.WriteValue(new DateTimeRangeDto(range).ToJson());
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.Value == null) return null;
        if (string.IsNullOrWhiteSpace((string)reader.Value) ||
            IsEqual(reader.Value.ToString(), "null")) return null;
        var fromJson = reader.Value.ToString().FromJson<DateTimeRangeDto>();
        return new DateTimeRange(fromJson.Start, fromJson.End);
    }

    public override bool CanConvert(Type objectType)
        => objectType == typeof(DateTimeRange);


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