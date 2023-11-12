[![.NET](https://github.com/neoxack/CQRS/actions/workflows/dotnet.yml/badge.svg)](https://github.com/neoxack/CQRS/actions/workflows/dotnet.yml)
[![NuGet](https://img.shields.io/nuget/v/Neoxack.CQRS.svg)](https://www.nuget.org/packages/Neoxack.CQRS)
[![NuGet](https://img.shields.io/nuget/dt/Neoxack.CQRS.svg)](https://www.nuget.org/packages/Neoxack.CQRS)

# CQRS

This library provides a .NET implementation of the CQRS pattern, allowing you to separate command and query responsibilities in your applications. CQRS promotes a clear separation between the command side that performs operations and the query side that retrieves data.

## Benchmarks

```
BenchmarkDotNet v0.13.10, macOS Sonoma 14.0 (23A344) [Darwin 23.0.0]
Apple M2 Pro, 1 CPU, 12 logical and 12 physical cores
.NET SDK 8.0.100-rc.2.23502.2
[Host]     : .NET 6.0.20 (6.0.2023.32017), Arm64 RyuJIT AdvSIMD
DefaultJob : .NET 6.0.20 (6.0.2023.32017), Arm64 RyuJIT AdvSIMD
```

| Method  |      Mean |    Error |   StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|---------|----------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
| CQRS    |  78.22 ns | 0.106 ns | 0.094 ns |  1.00 |    0.00 | 0.0114 |      24 B |        1.00 |
| MediatR | 195.33 ns | 1.608 ns | 1.504 ns |  2.50 |    0.02 | 0.1760 |     368 B |       15.33 |



## Contributing

Contributions are welcome! Please feel free to submit issues or pull requests.