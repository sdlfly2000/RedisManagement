using Common.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Core.ServiceRegistration;

public class ServiceRegister
{
    public static IServiceCollection Register(params string[] assemblies)
    {
        var serviceCollection = new ServiceCollection();
        if (assemblies.Length > 0)
        {
            serviceCollection.RegisterDomain(assemblies);
        }
        serviceCollection.AddMemoryCache();
        return serviceCollection;
    }
}
