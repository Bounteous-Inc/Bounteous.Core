using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Bounteous.Core.Serialization;

public class LongNameContractResolver : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        // Return the original property name
        return name;
    }

    public IList<JsonProperty> CreateProperties(Type type, JsonSerializerOptions options)
    {
        // Create properties using the default naming policy

        return type.GetProperties().Select(property => new JsonProperty
            { PropertyName = property.Name, UnderlyingName = property.Name }).ToList();
    }
}

public class JsonProperty
{
    public string PropertyName { get; set; }
    public string UnderlyingName { get; set; }
}