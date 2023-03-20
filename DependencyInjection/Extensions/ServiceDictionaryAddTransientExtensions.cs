namespace DependencyInjection.Extensions
{
    using Accessors;
    using Factories;

    public static class ServiceDictionaryAddTransientExtensions
    {
        public static IDictionary<int, IServiceAccessor> AddTransient(this IDictionary<int, IServiceAccessor> serviceAccessorDictionary, Type serviceType, IServiceFactory implementationFactory) =>
            ServiceDictionaryAddExtensions.Add(serviceAccessorDictionary, serviceType, new ServiceAccessor(implementationFactory));

        public static IDictionary<int, IServiceAccessor> AddTransient<TService>(this IDictionary<int, IServiceAccessor> serviceAccessorDictionary, IServiceFactory implementationFactory) =>
            AddTransient(serviceAccessorDictionary, typeof(TService), implementationFactory);

        public static IDictionary<int, IServiceAccessor> AddTransient<TService>(this IDictionary<int, IServiceAccessor> serviceAccessorDictionary) =>
            AddTransient<TService, TService>(serviceAccessorDictionary);

        public static IDictionary<int, IServiceAccessor> AddTransient<TService, TImplementation>(this IDictionary<int, IServiceAccessor> serviceAccessorDictionary) =>
            AddTransient<TService>(serviceAccessorDictionary, typeof(TImplementation));

        public static IDictionary<int, IServiceAccessor> AddTransient<TService>(this IDictionary<int, IServiceAccessor> serviceAccessorDictionary, Type implementationType) =>
            AddTransient(serviceAccessorDictionary, typeof(TService), implementationType);

        public static IDictionary<int, IServiceAccessor> AddTransient(this IDictionary<int, IServiceAccessor> serviceAccessorDictionary, Type serviceType, Type implementationType) =>
            AddTransient(serviceAccessorDictionary, serviceType, new ReflectionServiceFactory(serviceAccessorDictionary, implementationType));

        public static IDictionary<int, IServiceAccessor> AddTransient(this IDictionary<int, IServiceAccessor> serviceAccessorDictionary, Type serviceType, Func<IServiceProvider, object?> implementationFactory) =>
            AddTransient(serviceAccessorDictionary, serviceType, new FuncServiceFactory<object>(implementationFactory));

        public static IDictionary<int, IServiceAccessor> AddTransient<TService, TImplementation>(this IDictionary<int, IServiceAccessor> serviceAccessorDictionary, Func<IServiceProvider, TImplementation?> implementationFactory) =>
            AddTransient(serviceAccessorDictionary, typeof(TService), new FuncServiceFactory<TImplementation>(implementationFactory));

        public static IDictionary<int, IServiceAccessor> AddTransient<TService>(this IDictionary<int, IServiceAccessor> serviceAccessorDictionary, Func<IServiceProvider, TService?> implementationFactory) =>
            AddTransient<TService, TService>(serviceAccessorDictionary, implementationFactory);
    }
}