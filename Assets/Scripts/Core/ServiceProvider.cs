using System;
using System.Collections.Generic;

public class ServiceProvider : IServiceProvider {
    private readonly Dictionary<Type, object> services = new();

    public void Register<T>(T service) where T : class {
        services[typeof(T)] = service;
    }

    public T Get<T>() where T : class {
        if (services.TryGetValue(typeof(T), out var service)) {
            return (T)service;
        }
        throw new Exception($"Service {typeof(T)} not found!");
    }
}