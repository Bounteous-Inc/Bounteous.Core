using Bounteous.Core.Cache;
using Microsoft.Extensions.DependencyInjection;

namespace Bounteous.Core;

public sealed class ConfigureServiceCollection(IServiceCollection collection)
{
    public void Initialize() 
        => collection.AddSingleton<ICache>(new WaitToFinishMemoryCache());
}