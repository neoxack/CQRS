[![.NET](https://github.com/neoxack/CQRS/actions/workflows/dotnet.yml/badge.svg)](https://github.com/neoxack/CQRS/actions/workflows/dotnet.yml)
[![NuGet](https://img.shields.io/nuget/v/Neoxack.CQRS.svg)](https://www.nuget.org/packages/Neoxack.CQRS)
[![NuGet](https://img.shields.io/nuget/dt/Neoxack.CQRS.svg)](https://www.nuget.org/packages/Neoxack.CQRS)

# CQRS

This library provides a .NET implementation of the CQRS pattern, allowing you to separate command and query responsibilities in your applications. CQRS promotes a clear separation between the command side that performs operations and the query side that retrieves data.

## Installing CQRS

You should install [Neoxack.CQRS with NuGet](https://www.nuget.org/packages/Neoxack.CQRS):

    Install-Package Neoxack.CQRS

Or via the .NET Core command line interface:

    dotnet add package Neoxack.CQRS

Either commands, from Package Manager Console or .NET Core CLI, will download and install Neoxack.CQRS and all required dependencies.

### Registering with `IServiceCollection`

CQRS supports `Microsoft.Extensions.DependencyInjection.Abstractions` directly. To register CQRS services and handlers:

```
services.AddCQRS(typeof(Startup).Assembly);
```

This registers:

- `ICommandDispather` as singleton
- `IQueryDispatcher` as singleton
- `ICommandHandler<,>` concrete implementations as scoped
- `IQueryHandler<,>` concrete implementations as scoped

## Benchmarks

```
BenchmarkDotNet v0.13.10, macOS Sonoma 14.1.2 (23B92) [Darwin 23.1.0]
Apple M2 Pro, 1 CPU, 12 logical and 12 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 6.0.20 (6.0.2023.32017), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 6.0.20 (6.0.2023.32017), Arm64 RyuJIT AdvSIMD
```

| Method       |      Mean |    Error |   StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------------|----------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
| Neoxack.CQRS |  73.89 ns | 0.167 ns | 0.140 ns |  1.00 |    0.00 | 0.0114 |      24 B |        1.00 |
| MediatR      | 189.76 ns | 2.430 ns | 2.154 ns |  2.57 |    0.02 | 0.1760 |     368 B |       15.33 |



## Contributing

Contributions are welcome! Please feel free to submit issues or pull requests.