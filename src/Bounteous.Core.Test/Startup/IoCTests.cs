using System;
using Bounteous.Core.TestSupport;
using Bounteous.Core.Validations;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Xunit;

namespace Bounteous.Core.Test.Startup
{
    public class IoCTests : IDisposable
    {
        private readonly ServiceCollection collection;
        public IoCTests()
        {
            collection = new ServiceCollection();
            IoC.ConfigureServiceCollection(collection);
        }

        public void Dispose()
            => collection.Clear();

        [Fact]
        public void ApplicationConfig()
        {
            var appConfig = IoC.Resolve<IApplicationConfig>();

            Validate.Begin()
                .IsNotNull(appConfig, "has an app config")
                .Check()
                .IsEqual(appConfig.AllowedHosts, "*", "got allowedHosts")
                .IsEqual(appConfig.ConnectionString, "connectMe", nameof(IApplicationConfig.ConnectionString))
                .Check();
        }

        [Fact]
        public void UsingAnotherServiceCollection()
        {
            var privateCollection = new ServiceCollection();
            IoC.ConfigureServiceCollection(privateCollection);

            IoC.Resolve<IApplicationConfig>().Should().BeOfType<ApplicationConfig>();
            IoC.Resolve<IAddMe>().Should().BeOfType<AddMe>();
        }

        [Fact]
        public void MyService()
        {
            Validate.Begin().IsTrue(true, "true");
            collection.AddSingleton<IAddMe, AddMe>();
            
            var service = IoC.Resolve<IAddMe>();
            service.Should().NotBeNull();
            IoC.Resolve<IAddMe>().Should().BeOfType<AddMe>();
        }

        [Fact]
        public void ResolveShouldReturnTheSameInstance()
        {
            collection.AddSingleton<IDependency, DefaultDependency>();
            
            var service = IoC.Resolve<IDependency>();
            IoC.Resolve<IDependency>().Should().BeSameAs(service);
        }

        [Fact]
        public void TryResolveWithFindsDefault()
        {
            var privateCollection = new ServiceCollection();
            IoC.ConfigureServiceCollection(privateCollection);
            
            IoC.Resolve<IAddMe>().Should().BeOfType<AddMe>();
            IoC.TryResolve<IDependency, DefaultDependency>()
                .Should().BeOfType<DefaultDependency>();
        }
    }
}