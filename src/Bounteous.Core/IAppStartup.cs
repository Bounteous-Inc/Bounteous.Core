using System;
using Bounteous.Core.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bounteous.Core;

public interface IAppStartup
{
    IConfiguration StartUp(IServiceCollection collection);

    [Obsolete("use InitializeLogging() instead")]
    void InitializeLogging(IConfiguration configuration, Action<IConfiguration> defaultConfig)
        => LogStartup.Initialize(configuration);

    void InitializeLogging(Action defaultConfig);
}