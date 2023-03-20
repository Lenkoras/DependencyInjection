namespace DependencyInjection.Extensions
{
    using Accessors;
    using Factories;

    public static class ServiceDictionaryAddSingletonExtensions
    {
        public static IDictionary<int, IServiceAccessor> AddSingleton(this IDictionary<int, IServiceAccessor> serviceAccessorDictionary, Type serviceType, IServiceFactory implementationFactory) =>
            ServiceDictionaryAddExtensions.Add(serviceAccessorDictionary, serviceType, new SingletonServiceAccessor(implementationFactory));

        public static IDictionary<int, IServiceAccessor> AddSingleton<TService>(this IDictionary<int, IServiceAccessor> serviceAccessorDictionary, IServiceFactory implementationFactory) =>
            serviceAccessorDictionary.AddSingleton(typeof(TService), implementationFactory);

        public static IDictionary<int, IServiceAccessor> AddSingleton<TService>(this IDictionary<int, IServiceAccessor> serviceAccessorDictionary) =>
            serviceAccessorDictionary.AddSingleton<TService, TService>();

        public static IDictionary<int, IServiceAccessor> AddSingleton<TService, TImplementation>(this IDictionary<int, IServiceAccessor> serviceAccessorDictionary) =>
            serviceAccessorDictionary.AddSingleton<TService>(typeof(TImplementation));

        public static IDictionary<int, IServiceAccessor> AddSingleton<TService>(this IDictionary<int, IServiceAccessor> serviceAccessorDictionary, Type implementationType) =>
            serviceAccessorDictionary.AddSingleton(typeof(TService), implementationType);

        public static IDictionary<int, IServiceAccessor> AddSingleton(this IDictionary<int, IServiceAccessor> serviceAccessorDictionary, Type serviceType, Type implementationType) =>
            serviceAccessorDictionary.AddSingleton(serviceType, new ReflectionServiceFactory(serviceAccessorDictionary, implementationType));

        public static IDictionary<int, IServiceAccessor> AddSingleton(this IDictionary<int, IServiceAccessor> serviceAccessorDictionary, Type serviceType, Func<IServiceProvider, object?> implementationFactory) =>
            serviceAccessorDictionary.AddSingleton(serviceType, new FuncServiceFactory<object>(implementationFactory));

        public static IDictionary<int, IServiceAccessor> AddSingleton<TService, TImplementation>(this IDictionary<int, IServiceAccessor> serviceAccessorDictionary, Func<IServiceProvider, TImplementation> implementationFactory) =>
            serviceAccessorDictionary.AddSingleton(typeof(TService), new FuncServiceFactory<TImplementation>(implementationFactory));

        public static IDictionary<int, IServiceAccessor> AddSingleton<TService>(this IDictionary<int, IServiceAccessor> serviceAccessorDictionary, Func<IServiceProvider, TService?> implementationFactory) =>
            serviceAccessorDictionary.AddSingleton<TService, TService>(implementationFactory!);

        /// <summary>
        /// Adds a new service to the collection that returns specified <paramref name="constantService"/> by the <typeparamref name="TService"/> type.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="serviceAccessorDictionary"></param>
        /// <param name="constantService">Value that will return by type the <typeparamref name="TService"/>.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IDictionary<int, IServiceAccessor> AddSingleton<TService>(this IDictionary<int, IServiceAccessor> serviceAccessorDictionary, TService? constantService) =>
            AddSingleton(serviceAccessorDictionary, provider => constantService);
    }
}