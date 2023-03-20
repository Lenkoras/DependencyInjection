namespace DependencyInjection.Extensions
{
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// Gets the service object of the specified <typeparamref name="TService"/> type.
        /// </summary>
        /// <typeparam name="TService">A type of service object to get.</typeparam>
        /// <param name="serviceProvider">Service provider.</param>
        /// <returns>A service object of <see langword="typeof"/> <typeparamref name="TService"/>. -or- null if there is no service object of type serviceType.</returns>
        public static TService? GetService<TService>(this IServiceProvider serviceProvider) =>
            (TService?)serviceProvider.GetService(typeof(TService));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static TService GetRequiredService<TService>(this IServiceProvider serviceProvider)
        {
            var providerResult = serviceProvider.GetService(typeof(TService));
            if (providerResult is TService service)
            {
                return service;
            }
            throw new Exception($"Required service ({typeof(TService).FullName}) can not be resolved.");
        }
    }
}