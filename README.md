# Prism.Logging.Serilog [![NuGet Version](http://img.shields.io/nuget/v/Prism.Logging.Serilog.svg?style=flat)](https://www.nuget.org/packages/Prism.Logging.Serilog) [![Stack Overflow](https://img.shields.io/badge/stack%20overflow-serilog-orange.svg)](http://stackoverflow.com/questions/tagged/serilog) [![Stack Overflow](https://img.shields.io/badge/stack%20overflow-prism-orange.svg)](http://stackoverflow.com/questions/tagged/prism)

Integrate [Serilog](https://serilog.net) with [Prism](https://prismlibrary.github.io) in your WPF, UWP, or Xamarin Forms apps.

This project provides a custom implementation of Prism's `ILoggerFacade`, that forwards messages to a Serilog logger, allowing developers to capture the logging events written in their _ViewModels_ and _Services_, in Serilog.

## Give a Star! :star:

If you like or are using this project please give it a star. Thanks!

## Getting started

To use the `Prism.Logging.Serilog`, first install the [NuGet package](https://nuget.org/packages/prism.logging.serilog):

```powershell
Install-Package Prism.Logging.Serilog
```

Then register Serilog with Prism's `IContainerRegistry` using `RegisterSerilog()`:

```csharp
protected override void RegisterTypes(IContainerRegistry containerRegistry)
{
    // ...

    containerRegistry.RegisterSerilog();
}
```

Log events from Prism will be written to Serilog's `Log.Logger` by default. Alternatively, you can provide a specific instance of a `Serilog.ILogger`:

```csharp
private Serilog.ILogger _logger = Log.Logger;

protected override void RegisterTypes(IContainerRegistry containerRegistry)
{
    // ...

    containerRegistry.RegisterSerilog(_logger);
}
```

## Mapping of Prism Log messages to Serilog

`Prism.Logging.Serilog` does The Right Thing™ :), as you'd expect:

| Prism Category       | Serilog LogEventLevel       |
| -------------------- | --------------------------- |
| `Category.Debug`     | `LogEventLevel.Debug`       |
| `Category.Info`      | `LogEventLevel.Information` |
| `Category.Warn`      | `LogEventLevel.Warning`     |
| `Category.Exception` | `LogEventLevel.Error`       |

* The `Priority` set in log messages written via Prism gets forwarded to Serilog as a [context property](https://github.com/serilog/serilog/wiki/Writing-Log-Events#correlation) called `Priority`, with the value of the priority as a string. e.g. `"High"`.

* Log messages forwarded to Serilog have the [`SourceContext`](https://github.com/serilog/serilog/wiki/Writing-Log-Events#source-contexts) property set to `Prism.Logging.Serilog.SerilogLoggerFacade`, allowing developers to use use [filters](https://github.com/serilog/serilog/wiki/Configuration-Basics#filters), [sub-loggers](https://github.com/serilog/serilog/wiki/Configuration-Basics#sub-loggers), and [minimum level overrides](https://github.com/serilog/serilog/wiki/AppSettings#adding-minimum-level-overrides).

## Example

In the source code you can find a [demo project](sample) of a WPF application using Prism and Serilog. The initial setup looks something like this:

```csharp
public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        // Configure Serilog and the sinks at the startup of the app
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File(path: "MyApp.log")
            .CreateLogger();

        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        // Flush all Serilog sinks before the app closes
        Log.CloseAndFlush();

        base.OnExit(e);
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // Register your ViewModels, Services, etc...
        // ...

        // Register Serilog with Prism
        containerRegistry.RegisterSerilog();
    }

    protected override Window CreateShell()
    {
        return Container.Resolve<MainWindow>();
    }
}
```

## Release History

Click on the [Releases](https://github.com/augustoproiete/prism-logging-serilog/releases) tab on GitHub.

---

_Copyright &copy; 2019-2020 C. Augusto Proiete & Contributors - Provided under the [Apache License, Version 2.0](http://apache.org/licenses/LICENSE-2.0.html)._
