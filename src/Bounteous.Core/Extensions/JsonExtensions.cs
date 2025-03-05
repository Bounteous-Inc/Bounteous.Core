using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bounteous.Core.Extensions;

public static class JsonExtensions
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    public static string ToJson<T>(this T item, JsonSerializerOptions options = null)
        => JsonSerializer.Serialize(item, options ?? Options);

    public static T FromJson<T>(this string data, JsonSerializerOptions options = null)
        => JsonSerializer.Deserialize<T>(data, options ?? Options);
}