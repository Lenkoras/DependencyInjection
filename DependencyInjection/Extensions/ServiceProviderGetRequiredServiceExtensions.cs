namespace DependencyInjection.Extensions
{
    public static class ServiceProviderGetRequiredServiceExtensions
    {
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static object GetRequiredService(this IServiceProvider serviceProvider, Type serviceType)
        {
            ArgumentNullException.ThrowIfNull(serviceProvider);
            ArgumentNullException.ThrowIfNull(serviceType);

            var providerResult = serviceProvider.GetService(serviceType);
            if (providerResult is not null)
            {
                var resultType = providerResult.GetType();
                if (resultType.IsAssignableTo(serviceType))
                {
                    return providerResult;
                }
                else
                {
                    throw new Exception($"Required service ({serviceType.FullName}) can not be resolved. The service provider returned object with type ({resultType.FullName}) not assignable to {serviceType.FullName}.");
                }
            }
            throw new Exception($"Required service ({serviceType.FullName}) can not be resolved. The service provider returned null.");
        }

        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static TService GetRequiredService<TService>(this IServiceProvider serviceProvider) where TService : class =>
            (TService)GetRequiredService(serviceProvider, typeof(TService));
    }
}