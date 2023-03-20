namespace DependencyInjection.Accessors
{
    using Factories;

    public class ServiceAccessor : IServiceAccessor, IInitializable
    {
        private IServiceFactory serviceFactory;

        public ServiceAccessor(IServiceFactory serviceFactory)
        {
            ArgumentNullException.ThrowIfNull(serviceFactory);

            this.serviceFactory = serviceFactory;
        }

        public virtual object? GetService(IServiceProvider services)
        {
            return serviceFactory.Invoke(services);
        }

        public virtual void Initialize()
        {
            if (serviceFactory is IInitializable factory)
            {
                factory.Initialize();
            }
        }

    }
}