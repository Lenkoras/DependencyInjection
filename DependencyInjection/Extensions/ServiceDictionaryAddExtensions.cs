namespace DependencyInjection.Extensions
{
    using Accessors;

    public static class ServiceDictionaryAddExtensions
    {
        /// <summary>
        /// Adds a <paramref name="serviceAccessor"/> to the service collection with the <typeparamref name="TService"/> type.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="serviceAccessorDictionary"></param>
        /// <param name="serviceAccessor"></param>
        /// <returns></returns>
        public static IDictionary<int, IServiceAccessor> Add<TService>(this IDictionary<int, IServiceAccessor> serviceAccessorDictionary, IServiceAccessor serviceAccessor) =>
            Add(serviceAccessorDictionary, typeof(TService), serviceAccessor);

        /// <summary>
        /// Adds specified <paramref name="serviceAccessor"/> by provided <paramref name="serviceType"/> hash code.
        /// </summary>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IDictionary<int, IServiceAccessor> Add(this IDictionary<int, IServiceAccessor> serviceAccessorDictionary, Type serviceType, IServiceAccessor serviceAccessor)
        {
            ArgumentNullException.ThrowIfNull(serviceAccessorDictionary);
            ArgumentNullException.ThrowIfNull(serviceType);
            ArgumentNullException.ThrowIfNull(serviceAccessor);

            serviceAccessorDictionary.Add(serviceType.GetHashCode(), serviceAccessor);
            return serviceAccessorDictionary;
        }
    }
}