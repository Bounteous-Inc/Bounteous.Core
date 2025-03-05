using System.Text.Json;

namespace Bounteous.Core.Serialization;

public class SerializationSettings
{
    public static readonly JsonSerializerOptions LongNameSerializerOptions = new()
    {
        PropertyNamingPolicy = new LongNameContractResolver()
    };
}