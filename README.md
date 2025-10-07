# Bounteous.Core

A comprehensive .NET 8.0 library providing essential utilities, extensions, and patterns for modern .NET applications. This library simplifies common development tasks and provides robust, production-ready components for enterprise applications.

## 📦 Installation

Install the package via NuGet:

```bash
dotnet add package Bounteous.Core
```

Or via Package Manager Console:

```powershell
Install-Package Bounteous.Core
```

## 🚀 Quick Start

```csharp
using Bounteous.Core;
using Bounteous.Core.Extensions;
using Bounteous.Core.Time;

// Use dependency injection utilities
IoC.ConfigureServiceCollection(services);

// Use time utilities
var now = Clock.Utc.Now;
var localTime = Clock.Local.Now;

// Use string extensions
var result = "Hello World".ToBase64();
```

## 🏗️ Architecture Overview

Bounteous.Core follows modern .NET patterns and best practices:

- **Dependency Injection**: Built-in IoC container with auto-registration capabilities
- **Repository Pattern**: Abstract data access with Unit of Work support
- **Command Pattern**: Composite commands for complex operations
- **Strategy Pattern**: Pluggable algorithms and behaviors
- **Extension Methods**: Fluent APIs for common operations
- **Validation Framework**: Comprehensive validation with fluent syntax

## 🔧 Key Features

### Dependency Injection
- Auto-registration of services
- Service resolution with fallback defaults
- Scoped service management

### Data Access
- Repository pattern with Dapper integration
- Unit of Work for transaction management
- Connection builder abstraction

### Caching
- Memory cache with wait-to-finish semantics
- Configurable expiration policies
- Thread-safe cache operations

### Time Management
- Clock abstraction for testability
- Timezone-aware operations
- Time manipulation for testing

### Validation
- Fluent validation API
- Built-in validators for common scenarios
- Custom validation support

### Extensions
- String manipulation and encoding
- DateTime operations
- Enum utilities
- Collection extensions
- Reflection helpers

## 📚 Module Documentation

## Dependency Injection (IoC) Module

The IoC module provides a set of helpers to manage your `IServiceCollection` during application startup.

### IoC Class

#### ConfigureServiceCollection
Sets up the `IServiceCollection` to be used by the IoC container.

#### Resolve
Resolves and returns an instance of the specified service type.

#### TryResolve
Attempts to resolve an instance of the specified service type. If the service is not found, it returns a default implementation.

#### CreateScope
Creates and returns a new service scope, allowing for scoped service lifetimes.

#### Reset
Resets the IoC container with a new `IServiceCollection`, optionally provided by the caller.

### IoCExtensions Class

#### AutoRegister
Automatically registers all implementations of interfaces found in the specified assembly into the `IServiceCollection`.

#### AutoRegisterAll
Automatically registers all implementations of a specified type found in the specified assembly into the `IServiceCollection`.

#### FindAllFor
Finds and returns all implementations of a specified type within the specified assembly.

**Usage:**
```csharp
// Configure service collection
IoC.ConfigureServiceCollection(services);

// Resolve services
var service = IoC.Resolve<IMyService>();
var allServices = IoC.ResolveAll<IMyService>();

// Try resolve with fallback
var service = IoC.TryResolve<IMyService, DefaultMyService>();

// Create scope
using var scope = IoC.CreateScope();
var scopedService = scope.ServiceProvider.GetService<IMyService>();
```

## Cache Module

The Cache module provides caching strategies and implementations for .NET applications, with support for memory caching and configurable expiration policies.

### Core Interfaces

#### ICache
```csharp
public interface ICache
{
    Task<TItem> GetOrCreate<TItem>(object key, Func<Task<TItem>> createItem);
}
```

### Implementations

#### WaitToFinishMemoryCache
A thread-safe memory cache that ensures only one thread creates a value for a given key at a time.

**Features:**
- Thread-safe operations using `SemaphoreSlim`
- Configurable sliding and absolute expiration
- Memory pressure handling with size limits
- High priority for cache eviction

**Usage:**
```csharp
var cache = new WaitToFinishMemoryCache(
    slidingExpirationInMinutes: 2, 
    absoluteExpirationInMinutes: 15
);

var result = await cache.GetOrCreate("user:123", async () =>
{
    // This will only execute once per key, even with concurrent requests
    return await userService.GetUserAsync(123);
});
```

#### DisabledCache
A no-op implementation that always executes the factory method without caching.

**Usage:**
```csharp
var cache = new DisabledCache();

// Always executes the factory method
var result = await cache.GetOrCreate("key", async () =>
{
    return await expensiveOperation();
});
```

## Commands Module

The Commands module provides implementations of the Command pattern, enabling you to encapsulate operations as objects and compose them into complex workflows.

### Core Interfaces

#### ICommand
```csharp
public interface ICommand
{
    void Run();
}
```

#### IWaitedCommand
```csharp
public interface IWaitedCommand
{
    Task RunAsync();
}
```

#### ICommand<T>
```csharp
public interface ICommand<in TInput>
{
    Task RunAsync(TInput data);
}
```

### Implementations

#### CompositeCommand
Executes multiple synchronous commands in sequence.

**Usage:**
```csharp
var command = new CompositeCommand();
command.Add(new LogCommand("Starting process"));
command.Add(new ProcessDataCommand());
command.Add(new LogCommand("Process completed"));

command.Run(); // Executes all commands in order
```

#### CompositeWaitedCommand
Executes multiple async commands in sequence.

**Usage:**
```csharp
var command = new CompositeWaitedCommand();
command.Add(new LoadDataCommand());
command.Add(new ProcessDataCommand());
command.Add(new SaveDataCommand());

await command.RunAsync(); // Executes all commands in order
```

#### TypedCompositeCommand<T>
Executes multiple typed commands with the same input data.

**Usage:**
```csharp
var command = new TypedCompositeCommand<User>();
command.Add(new ValidateUserCommand());
command.Add(new EnrichUserCommand());
command.Add(new LogUserCommand());

await command.RunAsync(user); // Executes all commands with the same user data
```

### Extension Methods

#### Command Chaining
Use the `Then` extension method to chain commands fluently:

```csharp
// Synchronous chaining
var syncCommand = new LogCommand("Start")
    .Then(new ProcessCommand())
    .Then(new LogCommand("End"));

// Async chaining
var asyncCommand = new LoadDataCommand()
    .Then(new ProcessDataCommand())
    .Then(new SaveDataCommand());

// Typed chaining
var typedCommand = new ValidateUserCommand()
    .Then(new EnrichUserCommand())
    .Then(new LogUserCommand());
```

## Data Access Module

The Data Access module provides a robust data access layer using the Repository pattern with Unit of Work support, built on top of Dapper for high-performance data operations.

### Core Interfaces

#### IUnitOfWork
```csharp
public interface IUnitOfWork : IDisposable
{
    IDbTransaction Transaction { get; }
    IDbConnection Connection { get; }
    void Commit();
    void Rollback();
}
```

#### IUnitOfWorkProvider
```csharp
public interface IUnitOfWorkProvider
{
    Task<IUnitOfWork> CreateTransactional();
    Task<IUnitOfWork> CreateNonTransactional();
}
```

#### IConnectionBuilder
```csharp
public interface IConnectionBuilder
{
    Task<IDbConnection> CreateConnectionAsync();
    Task<IDbConnection> CreateReadConnectionAsync();
}
```

### Implementations

#### UnitOfWork
Manages database transactions with automatic rollback on disposal if not committed.

**Features:**
- Automatic transaction management
- Rollback on disposal if not committed
- Comprehensive logging with Serilog
- Exception handling with proper cleanup

**Usage:**
```csharp
using var uow = await unitOfWorkProvider.CreateTransactional();

try
{
    // Perform database operations
    await repository.InsertAsync(entity, uow.Connection, uow.Transaction);
    await repository.UpdateAsync(entity, uow.Connection, uow.Transaction);
    
    uow.Commit(); // Transaction committed successfully
}
catch
{
    // Transaction will be rolled back automatically
    throw;
}
// Unit of work disposed automatically
```

#### BaseRepository
Abstract base class providing common data access operations using Dapper.

**Usage:**
```csharp
public class UserRepository : BaseRepository
{
    public UserRepository(IConnectionBuilder connectionBuilder) 
        : base(connectionBuilder)
    {
    }
    
    public async Task<User> GetByIdAsync(int id)
    {
        const string sql = "SELECT * FROM Users WHERE Id = @Id";
        var results = await QueryAsync<User>(sql, new { Id = id });
        return results.FirstOrDefault();
    }
    
    public async Task<int> InsertAsync(User user, IDbConnection connection = null, IDbTransaction transaction = null)
    {
        const string sql = @"
            INSERT INTO Users (Name, Email, CreatedDate) 
            VALUES (@Name, @Email, @CreatedDate);
            SELECT CAST(SCOPE_IDENTITY() as int);";
            
        return await QueryAsync<int>(sql, user, connection, transaction).ContinueWith(t => t.Result.First());
    }
}
```

## Extensions Module

The Extensions module provides a comprehensive set of extension methods that enhance the functionality of common .NET types, making code more readable and reducing boilerplate.

### String Extensions

#### Encoding and Compression
```csharp
// Base64 encoding
var encoded = "Hello World".ToBase64();
var decoded = encoded.FromBase64();

// Compression
var compressed = "Large text content".Zip();
var decompressed = compressed.Unzip();

// Byte conversion
var bytes = "Hello".GetBytes();
var text = bytes.FromBytes();
```

#### String Manipulation
```csharp
// Default value handling
var result = stringValue.UseDefault("default value");

// Safe string operations
var safeValue = potentiallyNullString.SafeTrim();
```

### DateTime Extensions

#### Time Operations
```csharp
// Truncation
var truncated = DateTime.Now.TruncateMilliseconds();

// Range operations
var start = DateTime.Now.Earliest();
var end = DateTime.Now.Latest();

// Timezone conversions
var utc = localTime.ToUtc();
var local = utcTime.ToLocal();
```

### Enum Extensions

#### Description and Attributes
```csharp
// Get description from DescriptionAttribute
var description = MyEnum.Value.GetDescription();

// Get custom attributes
var attribute = MyEnum.Value.GetAttribute<MyCustomAttribute>();

// Parse from description
var parsed = EnumExtensions.ParseFromDescription<MyEnum>("Description Text");

// Sorted enumeration
var sortedValues = EnumExtensions.GetSortedList<MyEnum>();
```

### Collection Extensions

#### Enumerable Operations
```csharp
// Safe operations
var safeList = potentiallyNullEnumerable.EmptyIfNull();

// Conditional operations
var filtered = collection.WhereIf(condition, predicate);

// Batch operations
var batches = largeCollection.Batch(100);
```

### Reflection Extensions

#### Type Discovery
```csharp
// Find implementing types
var implementations = typeof(IMyInterface).GetImplementingTypes();

// Get parent assemblies
var parents = assembly.GetParentAssemblies();

// Property information from expressions
var propertyInfo = expression.GetProperty();
var propertyName = expression.NameOfProperty();
var propertyValue = expression.GetValue(myObject);
```

## Time Management Module

The Time Management module provides a comprehensive clock abstraction system that enables testable time-dependent code and supports multiple timezone operations.

### Core Interfaces

#### IClock
```csharp
public interface IClock
{
    DateTime Now { get; }
    DateTime NowUtc { get; }
    DateTime Today { get; }
    void Freeze();
    void Freeze(DateTime timeToFreeze);
    void Thaw();
}
```

### Static Clock Factory

#### Clock
Provides access to predefined clock instances for common timezones.

```csharp
// Available clock instances
var localTime = Clock.Local.Now;
var utcTime = Clock.Utc.Now;
var mountainTime = Clock.MountainTime.Now;
var centralTime = Clock.CentralTime.Now;
var australianTime = Clock.AustralianEasternTime.Now;

// Special values
var endOfTime = Clock.EndOfTime; // 9999-12-31 23:59:59.999

// Clock manipulation control
Clock.AllowClockManipulation = true; // Enable for testing
```

### Implementations

#### TimezoneClock
Timezone-aware clock implementation that converts between local and target timezones.

**Usage:**
```csharp
var mountainClock = new TimezoneClock(TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time"));

var localTime = mountainClock.Now;
var utcTime = mountainClock.NowUtc;
var today = mountainClock.Today;

// Convert to specific timezone
var pacificTime = mountainClock.NowToTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time"));

// Work with timezone offsets
var todayAtOffset = mountainClock.TodayAt(TimezoneOffset.Pacific);
var nowAtOffset = mountainClock.NowAt(TimezoneOffset.Mountain);
```

#### FreezeClock
Test clock that allows time manipulation for unit testing.

```csharp
var freezeClock = new FreezeClock();

// Freeze time
freezeClock.Freeze(new DateTime(2024, 1, 1, 12, 0, 0));
var frozenTime = freezeClock.Now; // Always returns frozen time

// Thaw time
freezeClock.Thaw();
var realTime = freezeClock.Now; // Returns current time
```

### Timezone Utilities

#### TimezoneOffset
Provides predefined timezone offsets for common US timezones.

```csharp
// Predefined offsets
var pacificOffset = TimezoneOffset.Pacific;   // -8
var mountainOffset = TimezoneOffset.Mountain; // -7
var centralOffset = TimezoneOffset.Central;   // -6
var easternOffset = TimezoneOffset.Eastern;   // -5

// Convert UTC to timezone
var pacificTime = pacificOffset.From(DateTime.UtcNow);
var todayInPacific = pacificOffset.TodayFrom(DateTime.UtcNow);
```

## Strategies Module

The Strategies module provides implementations of the Strategy pattern, enabling you to define families of algorithms and make them interchangeable at runtime.

### Core Interfaces

#### IStrategy<T>
```csharp
public interface IStrategy<T>
{
    Task<T> RunAsync(T subject);
}
```

### Implementations

#### CompositeStrategy<T>
Executes multiple strategies in sequence, passing the result of each strategy to the next one.

**Usage:**
```csharp
// Two strategies
var strategy = new CompositeStrategy<User>(
    new ValidateUserStrategy(),
    new EnrichUserStrategy()
);

// Multiple strategies
var multiStrategy = new CompositeStrategy<User>(
    new ValidateUserStrategy(),
    new EnrichUserStrategy(),
    new LogUserStrategy(),
    new CacheUserStrategy()
);

// Execute strategies
var result = await strategy.RunAsync(user);
```

### Extension Methods

#### Strategy Chaining
Use the `Then` extension method to chain strategies fluently:

```csharp
var strategy = new ValidateUserStrategy()
    .Then(new EnrichUserStrategy())
    .Then(new LogUserStrategy());
    
var result = await strategy.RunAsync(user);
```

### Example Implementations

#### Validation Strategy
```csharp
public class ValidateUserStrategy : IStrategy<User>
{
    public async Task<User> RunAsync(User user)
    {
        if (string.IsNullOrEmpty(user.Email))
            throw new ValidationException("Email is required");
            
        if (user.Age < 18)
            throw new ValidationException("User must be 18 or older");
            
        return user;
    }
}
```

#### Enrichment Strategy
```csharp
public class EnrichUserStrategy : IStrategy<User>
{
    private readonly IUserService userService;
    
    public EnrichUserStrategy(IUserService userService)
    {
        this.userService = userService;
    }
    
    public async Task<User> RunAsync(User user)
    {
        // Enrich user with additional data
        user.FullName = $"{user.FirstName} {user.LastName}";
        user.DisplayName = await userService.GenerateDisplayNameAsync(user);
        
        return user;
    }
}
```

## Serialization Module

The Serialization module provides utilities for JSON serialization with custom naming policies and specialized converters for common data types.

### Core Classes

#### SerializationSettings
Provides predefined JSON serialization options with custom naming policies.

```csharp
public class SerializationSettings
{
    public static readonly JsonSerializerOptions LongNameSerializerOptions = new()
    {
        PropertyNamingPolicy = new LongNameContractResolver()
    };
}
```

**Usage:**
```csharp
// Use predefined settings
var json = JsonSerializer.Serialize(data, SerializationSettings.LongNameSerializerOptions);

// Custom settings
var options = new JsonSerializerOptions
{
    PropertyNamingPolicy = new LongNameContractResolver(),
    WriteIndented = true
};
var json = JsonSerializer.Serialize(data, options);
```

#### LongNamingContractResolver
Custom JSON naming policy that preserves original property names without transformation.

**Usage:**
```csharp
var options = new JsonSerializerOptions
{
    PropertyNamingPolicy = new LongNamingContractResolver()
};

// Serializes with original property names
var json = JsonSerializer.Serialize(new { FirstName = "John", LastName = "Doe" }, options);
// Result: {"FirstName":"John","LastName":"Doe"}
```

#### DateTimeRangeConverter
Specialized JSON converter for `DateTimeRange` objects that handles serialization and deserialization with proper format.

**Usage:**
```csharp
var options = new JsonSerializerOptions
{
    Converters = { new DateTimeRangeConverter() }
};

var range = new DateTimeRange(DateTime.Now, DateTime.Now.AddDays(1));
var json = JsonSerializer.Serialize(range, options);
var deserialized = JsonSerializer.Deserialize<DateTimeRange>(json, options);
```

## Utilities Module

The Utilities module provides a comprehensive collection of utility classes and helpers that solve common programming problems and provide reusable functionality across different application domains.

### Core Utilities

#### Range<T>
Generic range class for working with ranges of comparable values.

**Usage:**
```csharp
// Basic range
var numberRange = new Range<int>(1, 100);
var isIncluded = numberRange.Includes(50); // true

// Date range
var dateRange = new Range<DateTime>(DateTime.Now, DateTime.Now.AddDays(30));
var isDateIncluded = dateRange.Includes(DateTime.Now.AddDays(15)); // true

// Custom incrementor
var dayRange = new Range<DateTime>(
    DateTime.Now, 
    DateTime.Now.AddDays(7),
    d => d.AddDays(1)
);

// Iterate through range
foreach (var day in dayRange.Iterate)
{
    Console.WriteLine(day);
}

// Range operations
var range1 = new Range<int>(1, 10);
var range2 = new Range<int>(5, 15);
var overlaps = range1.Overlaps(range2); // true
var includes = range1.Includes(range2); // false
```

#### ApplicationEvent
Application event tracking for monitoring and auditing.

**Usage:**
```csharp
var appEvent = new ApplicationEvent
{
    User = "john.doe",
    Operation = "ProcessOrder",
    Details = "Processing order #12345"
};

// Start timing
appEvent.StartEvent();

try
{
    // Perform operation
    await ProcessOrderAsync();
    
    appEvent.Outcome = Outcome.Successful;
}
catch (Exception ex)
{
    appEvent.Outcome = Outcome.Failed;
    appEvent.FailureCause = ex.Message;
}
finally
{
    // Stop timing and generate identifier
    appEvent.StopEvent();
    
    // Log the event
    Log.Information("Event completed: {Event}", appEvent);
}
```

#### PerformanceTracer
Performance monitoring utilities for measuring execution time.

**Usage:**
```csharp
// Synchronous performance tracing
var result = this.PerformanceTrace(() =>
{
    return ExpensiveOperation();
}, "ExpensiveOperation");

// Asynchronous performance tracing
await this.PerformanceTraceAsync(async () =>
{
    await ExpensiveAsyncOperation();
}, "ExpensiveAsyncOperation");

// With return value
var result = await this.PerformanceTraceAsync(async () =>
{
    return await ExpensiveAsyncOperationWithResult();
}, "ExpensiveAsyncOperationWithResult");
```

#### Endeavor
Retry and error handling utilities for resilient operations.

**Usage:**
```csharp
// Basic retry
await Endeavor.Go<HttpRequestException>(
    async () => await MakeHttpRequestAsync(),
    retries: 3,
    delay: 1000
);

// With custom exception handling
await Endeavor.Go<TimeoutException>(
    async () => await ProcessDataAsync(),
    async (ex) => 
    {
        Log.Warning("Timeout occurred, retrying...");
        await Task.Delay(500);
    },
    retries: 5,
    delay: 2000
);
```

### Object Mapping Framework

#### AbstractMapper<TFrom, TO>
Base class for creating object mappers with automatic property mapping.

**Usage:**
```csharp
public class UserMapper : AbstractMapper<UserDto, User>
{
    protected override void Initialize()
    {
        // Automatic mapping for same property names
        Map(u => u.FirstName, u => u.FirstName);
        Map(u => u.LastName, u => u.LastName);
        Map(u => u.Email, u => u.Email);
        
        // Custom mapping
        Map(u => u.FullName, u => $"{u.FirstName} {u.LastName}");
        
        // Type conversion
        Map(u => u.CreatedDate, u => DateTime.Parse(u.CreatedDateString));
    }
    
    protected override User Create()
    {
        return new User();
    }
}

// Usage
var mapper = new UserMapper();
var user = mapper.Build(userDto);
```

## Validations Module

The Validations module provides a comprehensive validation framework with fluent syntax, built-in validators for common scenarios, and support for custom validation rules.

### Core Classes

#### Validation
Main validation container that collects and manages validation exceptions.

**Usage:**
```csharp
var validation = new Validation();

// Add validation exceptions
validation.Add(new ValidationException("Field is required"));
validation.Add(new ValidationException("Invalid format").Warning());

// Check validation state
var isValid = validation.IsValid();
var errors = validation.Errors;
var warnings = validation.Warnings;

// Pretty print all issues
var message = validation.PrettyPrint();
```

#### ValidationException
Represents a validation error with severity levels and custom formatting.

**Usage:**
```csharp
// Basic validation exception
var exception = new ValidationException("Field is required");

// Warning level
var warning = new ValidationException("Field is deprecated").Warning();

// Static factory methods
var required = ValidationException.IsRequired("Email");
var tooLong = ValidationException.ExceedsMaximumLength("Name");
var invalidFormat = ValidationException.MustBeEmail("Email");
```

### Fluent Validation API

#### Basic Validation Methods
```csharp
var validation = Validate.Begin()
    .IsNotEmpty(value, "Field is required")
    .IsTrue(condition, "Condition must be true")
    .IsEqual(left, right, "Values must be equal")
    .IsNotEqual(left, right, "Values must not be equal")
    .Check();
```

#### String Validation
```csharp
var validation = Validate.Begin()
    .IsNotEmpty(email, "Email is required")
    .IsEmail(email, "Email format is invalid")
    .IsMinimumLength(name, 2, "Name must be at least 2 characters")
    .IsMaximumLength(description, 500, "Description cannot exceed 500 characters")
    .IsLengthOf(phone, 10, "Phone number must be exactly 10 digits")
    .Check();
```

#### Numeric Validation
```csharp
var validation = Validate.Begin()
    .IsDecimal(priceString, "Price must be a valid decimal")
    .IsPrice(priceString, "Price must be greater than 0")
    .IsInteger(quantityString, "Quantity must be a valid integer")
    .GreaterThan(age, 18, "Age must be greater than 18")
    .LessThan(score, 100, "Score must be less than 100")
    .IsBetween(value, 1, 100, "Value must be between 1 and 100")
    .Check();
```

#### Date Validation
```csharp
var validation = Validate.Begin()
    .IsDate(dateString, "Date format is invalid")
    .IsCloseEnough(startDate, endDate, TimeSpan.FromMinutes(5), "Dates must be within 5 minutes")
    .IsAfter(startDate, DateTime.Now, "Start date must be in the future")
    .IsBefore(endDate, DateTime.Now.AddYears(1), "End date must be within one year")
    .Check();
```

#### Collection Validation
```csharp
var validation = Validate.Begin()
    .IsNotEmpty(collection, "Collection cannot be empty")
    .IsNotNull(collection, "Collection cannot be null")
    .HasMinimumCount(items, 1, "Must have at least one item")
    .HasMaximumCount(items, 10, "Cannot have more than 10 items")
    .Check();
```

#### Custom Validation
```csharp
var validation = Validate.Begin()
    .IsTrue(customCondition, "Custom validation failed")
    .ContinueIfValid(v => v.IsNotEmpty(conditionalField, "Field required when condition is true"))
    .Check();
```

### Built-in Validators

#### Email Validation
```csharp
var validation = Validate.Begin()
    .IsEmail(email, "Email format is invalid")
    .Check();
```

#### Phone Number Validation
```csharp
var validation = Validate.Begin()
    .IsPhoneNumber(phone, "Phone number format is invalid")
    .Check();
```

#### Postal Code Validation
```csharp
var validation = Validate.Begin()
    .IsPostalCode(postalCode, "Postal code format is invalid")
    .Check();
```

#### Numeric Validation
```csharp
var validation = Validate.Begin()
    .IsNumeric(numericString, "Must be a valid number")
    .IsDecimal(decimalString, "Must be a valid decimal")
    .IsInteger(integerString, "Must be a valid integer")
    .Check();
```

### Custom Validation Rules

#### Creating Custom Validators
```csharp
public static class CustomValidationExtensions
{
    public static Validation IsValidPassword(this Validation validation, string password, string property)
    {
        return validation
            .IsNotEmpty(password, $"{property} is required")
            .IsMinimumLength(password, 8, $"{property} must be at least 8 characters")
            .IsTrue(password.Any(char.IsUpper), $"{property} must contain uppercase letter")
            .IsTrue(password.Any(char.IsLower), $"{property} must contain lowercase letter")
            .IsTrue(password.Any(char.IsDigit), $"{property} must contain digit");
    }
}

// Usage
var validation = Validate.Begin()
    .IsValidPassword(password, "Password")
    .Check();
```

## 🎯 Target Framework

- **.NET 8.0** and later

## 📋 Dependencies

- **Dapper** (2.1.66) - Micro ORM
- **Microsoft.Extensions.Caching.Memory** (9.0.9) - Memory caching
- **Microsoft.Extensions.Configuration** (9.0.9) - Configuration management
- **Microsoft.Extensions.DependencyInjection** (9.0.9) - Dependency injection
- **Serilog** (4.3.0) - Structured logging
- **System.Text.Json** (9.0.9) - JSON serialization

## 🤝 Contributing

This library is maintained by Xerris Inc. For contributions, please contact the development team.

## 📄 License

See [LICENSE](LICENSE) file for details.

## 🔗 Related Projects

- [Sample API](sample/Bounteous.Sample.Api/) - Example implementation
- [Test Suite](src/Bounteous.Core.Test/) - Comprehensive test coverage
- [Test Support](src/Bounteous.Core.TestSupport/) - Testing utilities

---

*This comprehensive documentation covers all major features and modules of the Bounteous.Core library, providing both high-level overview and detailed implementation guidance for .NET engineers.*