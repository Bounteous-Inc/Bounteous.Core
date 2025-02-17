using System;
using Bounteous.DotNet.Core.TestSupport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bounteous.DotNet.Core.Test
{
    public class ApplicationStartup : IAppStartup
    {
        public IConfiguration StartUp(IServiceCollection collection)
        {
            var builder = new ApplicationConfigurationBuilder<ApplicationConfig>();
            var appConfig = builder.Build();

            collection.AddSingleton<IApplicationConfig>(appConfig);
            collection.AddSingleton<IAddMe, AddMe>();

            collection.AutoRegister(GetType().Assembly).AutoRegister(typeof(IAddMe).Assembly);
            return builder.Configuration;
        }

        public void InitializeLogging(IConfiguration configuration, Action<IConfiguration> defaultConfig)
            => defaultConfig(configuration);
    }
}