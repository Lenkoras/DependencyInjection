namespace DependencyInjection.Extensions
{
    public static class ServiceProviderGetServiceExtensions
    {
        /// <summary>
        /// Gets the service object of the specified <typeparamref name="TService"/> type.
        /// </summary>
        /// <typeparam name="TService">A type of service object to get.</typeparam>
        /// <param name="serviceProvider">Service provider.</param>
        /// <returns>A service object of <see langword="typeof"/> <typeparamref name="TService"/>. -or- null if there is no service object of type serviceType.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TService? GetService<TService>(this IServiceProvider serviceProvider) where TService : class
        {
            ArgumentNullException.ThrowIfNull(serviceProvider);

            return (TService?)serviceProvider.GetService(typeof(TService));
        }
    }
}