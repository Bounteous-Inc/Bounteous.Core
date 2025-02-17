using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bounteous.Core;

public interface IAppStartup
{
    IConfiguration StartUp(IServiceCollection collection);
    void InitializeLogging(IConfiguration configuration, Action<IConfiguration> defaultConfig);
}