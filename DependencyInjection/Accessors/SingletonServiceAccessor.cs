namespace DependencyInjection.Accessors
{
    using Factories;

    public sealed class SingletonServiceAccessor : ServiceAccessor
    {
        private object? value;
        private bool initialized;
        private object threadAccess;

        public SingletonServiceAccessor(IServiceFactory serviceFactory) : base(serviceFactory)
        {
            initialized = false;
            threadAccess = new();
        }

        /// <summary>
        /// Returns thread-safe singleton service.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public override object? GetService(IServiceProvider services)
        {
            lock (threadAccess)
            {
                if (!initialized)
                {
                    value = base.GetService(services);
                    initialized = true;
                }
                return value;
            }
        }
    }
}