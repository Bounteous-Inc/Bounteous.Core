# IoC Module

The IoC module provides a set of helpers to manage your `IServiceCollection` during application startup.


## IoC Class

### ConfigureServiceCollection

Sets up the `IServiceCollection` to be used by the IoC container.


### Resolve

Resolves and returns an instance of the specified service type.


### TryResolve

Attempts to resolve an instance of the specified service type. If the service is not found, it returns a default implementation.


### CreateScope

Creates and returns a new service scope, allowing for scoped service lifetimes.


### Reset

Resets the IoC container with a new `IServiceCollection`, optionally provided by the caller.


## IoCExtensions Class

### AutoRegister

Automatically registers all implementations of interfaces found in the specified assembly into the `IServiceCollection`.


### AutoRegisterAll

Automatically registers all implementations of a specified type found in the specified assembly into the `IServiceCollection`.


### FindAllFor

Finds and returns all implementations of a specified type within the specified assembly.
