# Windows Forms Generic Host

WindowsFormsGenericHost is a simple library that allows you to use the [.NET Generic Host](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host) model with Windows Forms applications, on both .NET Core and .NET Framework.

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
