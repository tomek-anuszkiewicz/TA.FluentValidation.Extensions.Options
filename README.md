With this package you can easily validate your configuration with [FluentValidation](https://fluentvalidation.net/). 
You may also opt-in to trigger validation on startup. 

![](https://dev.azure.com/tomekanuszkiewicz/TA.FluentValidation.Extensions.Options/_apis/build/status/tomek-anuszkiewicz.TA.FluentValidation.Extensions.Options?branchName=master) 
&nbsp;
![Azure DevOps tests](https://img.shields.io/azure-devops/tests/tomekanuszkiewicz/TA.FluentValidation.Extensions.Options/1)
&nbsp;
![Azure DevOps coverage](https://img.shields.io/azure-devops/coverage/tomekanuszkiewicz/TA.FluentValidation.Extensions.Options/1)
&nbsp;
![GitHub](https://img.shields.io/github/license/tomek-anuszkiewicz/TA.FluentValidation.Extensions.Options)

### 1. Prerequisites

Configuration is configured. 
See [Configuration in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration)

Options and configuration are bound. See [Options pattern in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options)

Code example: 

```csharp
services
    .AddOptions<SomeSettings>()
    .Bind(Configuration.GetSection(nameof(SomeSettings)));
```

So basically you can ask for `IOptions<TOptions>` from container (where class TOptions is bound to configuration section).

You have FluentValidator for `TOptions`. See [Creating your first validator](https://docs.fluentvalidation.net/en/latest/start.html)

FluentValidator is registered in container. You may use [ASP.NET Core integration](https://docs.fluentvalidation.net/en/latest/aspnet.html) for this.

Code example: 

```csharp
services
    .AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
```

Note: In examples on page [ASP.NET Core integration](https://docs.fluentvalidation.net/en/latest/aspnet.html)
we see: `services.AddMvc`, it's also works with `services.AddControllers()` in 
case `API project` was created.


### 2. Use validator to validate configuration

Chain [OptionsBuilder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.optionsbuilder-1)
with `ValidateWithFluentValidation`:

```csharp
services
  .AddOptions<PersonSettings>()
  .Bind(Configuration.GetSection(nameof(PersonSettings)))
  .ValidateWithFluentValidation();
```

### 3. Validate on startup

Add line:

```csharp
services.AddStartupFilterToValidateOptionsWithFluentValidation();
```

### 4. Notes

Normally validation occurs once with `IOptions` or once per request with `IOptionsSnapshot`.
Validation is triggered when you access `Value` property.
When using [`IOptions`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.ioptions-1) 
result [ValidateOptionsResult](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.validateoptionsresult) is cached and used on every request.

In examples on page [Options pattern in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options)
you may find

```
services.Configure<MyOptions>(Configuration);
```

, this syntax don't give access to fluent [OptionsBuilder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.optionsbuilder-1)
and thus to configure validation.

On my machine I had some trouble to run sample `ValidationOnStartup` in debug. Application hung up.
