# Windows Forms Generic Host

WindowsFormsGenericHost is a simple library that allows you to use the [.NET Generic Host](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host) model with Windows Forms applications, on both .NET Core and .NET Framework.

## Credit and inspiration

This project was inspired by Alex Oswald's [WindowsFormsLifetime](https://github.com/alex-oswald/WindowsFormsLifetime) project. It was released soon after his project, with the aims of adding support for ApplicationContext, and having a slightly different API and implementation. It also borrows heavily from [Microsoft.Extensions.Hosting's ConsoleLifetime](https://github.com/aspnet/Hosting/blob/master/src/Microsoft.Extensions.Hosting/Internal/ConsoleLifetime.cs)

I'd recommend using the [WindowsFormsLifetime](https://github.com/alex-oswald/WindowsFormsLifetime) instead of this project, as it was the original and is more actively maintained. However, if this project suits your use case more, then feel free to use this one.

## Quickstart

Set up your `Program.cs` in a similar manner to this:

```csharp
static class Program
{
    public static void Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder()
            .ConfigureServices(ConfigureServices)
            .UseWindowsFormsLifetime<MainForm>()
            .Build();

        host.Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddForms();

        services.AddYourOtherServicesHere();
    }
}
```

## More examples

Check out the [samples](/samples)

## Installation

Just grab it from [NuGet](https://www.nuget.org/packages/WindowsFormsGenericHost/)

```
PM> Install-Package WindowsFormsGenericHost
```

```
$ dotnet add package WindowsFormsGenericHost
```

## License and copyright

Copyright Matthew King.
Distributed under the [MIT License](http://opensource.org/licenses/MIT).
Refer to license.txt for more information.
