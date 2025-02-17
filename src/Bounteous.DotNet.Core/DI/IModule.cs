using Microsoft.Extensions.DependencyInjection;

namespace Bounteous.DotNet.Core.DI;

public interface IModule
{
    int Priority { get; }
    void RegisterServices(IServiceCollection services);
}