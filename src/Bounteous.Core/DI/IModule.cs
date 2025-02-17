using Microsoft.Extensions.DependencyInjection;

namespace Bounteous.Core.DI;

public interface IModule
{
    int Priority { get; }
    void RegisterServices(IServiceCollection services);
}