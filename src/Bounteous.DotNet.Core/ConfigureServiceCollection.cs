using Bounteous.DotNet.Core.Cache;
using Microsoft.Extensions.DependencyInjection;

namespace Bounteous.DotNet.Core;

public sealed class ConfigureServiceCollection
{
    private readonly IServiceCollection collection;

    public ConfigureServiceCollection(IServiceCollection collection) => this.collection = collection;


    public void Initialize() => collection.AddSingleton<ICache>(new WaitToFinishMemoryCache());
}