using Bounteous.Core.DI;
using Microsoft.Extensions.DependencyInjection;

namespace Bounteous.Core.TestSupport;

public class ModuleStartUp : IModule
{
    public int Priority => 1;
    public void RegisterServices(IServiceCollection services)
    {
        services.AutoRegisterAll<IAutoRegisterAll>(GetType().Assembly);
    }
}