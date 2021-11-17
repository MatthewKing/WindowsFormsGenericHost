using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace WindowsFormsGenericHost;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddForms(this IServiceCollection services, Assembly assembly = null, ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (assembly == null)
        {
            assembly = Assembly.GetCallingAssembly();
        }

        var formType = typeof(Form);
        var formImplementationTypes = assembly.GetTypes()
            .Where(x => formType.IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface)
            .ToArray();

        foreach (var formImplementationType in formImplementationTypes)
        {
            var descriptor = new ServiceDescriptor(formImplementationType, formImplementationType, lifetime);
            services.Add(descriptor);
        }

        return services;
    }
}
