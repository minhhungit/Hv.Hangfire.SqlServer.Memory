# Hv.Hangfire.SqlServer.Memory <a href="https://www.nuget.org/packages/Hv.Hangfire.SqlServer.Memory/"><img src="https://img.shields.io/nuget/v/Hv.Hangfire.SqlServer.Memory.svg?style=flat" /> </a>
Memory queues support for SQL Server job storage implementation for Hangfire

> Note: This library has not completed yet, I'm still working on Transaction

---

### Installation
```powershell
Install-Package Hv.Hangfire.SqlServer.Memory
```

### Configuration
If you are using only default queue, call the UseMemoryQueues method just after UseSqlServerStorage method call and pass the path pattern as an argument.

```csharp
GlobalConfiguration.Configuration
    .UseSqlServerStorage("<connection string or its name>")
    .UseMemoryQueues(@"hangfire-{0}");
```

To use multiple queues, you should pass them explicitly:

```csharp
GlobalConfiguration.Configuration
    .UseSqlServerStorage("<connection string or its name>")
    .UseMemoryQueues(@"hangfire-{0}", "critical", "default");
```
