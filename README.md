# Hv.Hangfire.SqlServer.Memory
Memory queues support for SQL Server job storage implementation for Hangfire

### Installation
>Install-Package Hv.Hangfire.SqlServer.Memory

### Configuration
```csharp
GlobalConfiguration.Configuration
    .UseSqlServerStorage("<connection string or its name>")
    .UseMemoryQueues(@"my-memory-{0}");
```

To use multiple queues, you should pass them explicitly:

```csharp
GlobalConfiguration.Configuration
    .UseSqlServerStorage("<connection string or its name>")
    .UseMemoryQueues(@"my-memory-{0}", "critical", "default");
```
