using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Bounteous.Core.TestSupport;
using AwesomeAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Bounteous.Core.Test.DI
{
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
    public class IoCTests
    {
        public IoCTests() => IoC.Reset();

        [Fact]
        public void ConfigureServiceCollection_ShouldSetServiceCollectionProvider()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            IoC.ConfigureServiceCollection(services);

            // Assert
            var serviceProvider = IoC.CreateScope().ServiceProvider;
            serviceProvider.Should().NotBeNull();
        }

        [Fact]
        public void Resolve_ShouldReturnRegisteredService()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddSingleton<IService, ServiceImplementation>();
            IoC.Reset(services);

            // Act
            var service = IoC.Resolve<IService>();

            // Assert
            service.Should().NotBeNull();
            service.Should().BeOfType<ServiceImplementation>();
        }

        [Fact]
        public void TryResolve_ShouldReturnRegisteredService()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddSingleton<IService, ServiceImplementation>();
            IoC.Reset(services);

            // Act
            var service = IoC.TryResolve<IService, DefaultServiceImplementation>();

            // Assert
            service.Should().NotBeNull();
            service.Should().BeOfType<ServiceImplementation>();
        }

        [Fact]
        public void TryResolve_ShouldReturnDefaultService_WhenNotRegistered()
        {
            // Arrange
            var services = new ServiceCollection();
            IoC.Reset(services);
            _ = IoC.Resolve<IApplicationConfig>();

            // Act
            var service = IoC.TryResolve<IService, DefaultServiceImplementation>();

            // Assert
            service.Should().NotBeNull();
            service.Should().BeOfType<DefaultServiceImplementation>();
        }

        [Fact]
        public void CreateScope_ShouldReturnNewServiceScope()
        {
            // Arrange
            var services = new ServiceCollection();
            IoC.Reset(services);

            // Act
            using var scope = IoC.CreateScope();
            // Assert
            scope.Should().NotBeNull();
            scope.ServiceProvider.Should().NotBeNull();
        }
        
        [Fact]
        public void FindAll_ShouldReturnAllImplementationsOfIMyService()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddTransient<IService, ServiceImplementation>();
            services.AddTransient<IService, DefaultServiceImplementation>();

            // Act
            IoC.ConfigureServiceCollection(services);
            var myServices = IoC.ResolveAll<IAutoRegisterAll>();

            // Assert
            Assert.NotNull(myServices);
            Assert.Equal(3, myServices.Count());
            Assert.Contains(myServices, service => service is AutoRegister1);
            Assert.Contains(myServices, service => service is AutoRegister1);
            Assert.Contains(myServices, service => service is AutoRegister3);
            Assert.DoesNotContain(myServices, service => service is AutoRegisterIgnored);
        }

        [Fact]
        public void FindAll_Ignores_Class_WithAttribute_IgnoreIoCRegistration()
        {
            
        }
        
    }
}