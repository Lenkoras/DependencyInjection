namespace DependencyInjection.Extensions
{
    public static class ServiceProviderGetRequiredServiceExtensions
    {
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static object GetRequiredService(this IServiceProvider serviceProvider, Type serviceType)
        {
            return serviceProvider.GetService(serviceType);
        }

        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static TService GetRequiredService<TService>(this IServiceProvider serviceProvider) where TService : class =>
            (TService)GetRequiredService(serviceProvider, typeof(TService));
    }
}