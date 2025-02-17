using Newtonsoft.Json;

namespace Bounteous.Core.Serialization;

public class SerializationSettings
{
    public static readonly JsonSerializerSettings LongNameSerializerSettings =
        new() { ContractResolver = new LongNameContractResolver() };
}