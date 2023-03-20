namespace DependencyInjection.Extensions
{
    using Accessors;
    using DependencyInjection.Rules;

    public static class serviceAccessorDictionaryBuildServiceProviderExtensions
    {
        public static IServiceProvider BuildServiceProvider(this IDictionary<int, IServiceAccessor> serviceAccessorDictionary, ICollection<IRule> ruleCollection)
        {
            InitializeServiceAccessors(serviceAccessorDictionary.Values);
            return new ServiceProvider(serviceAccessorDictionary, ruleCollection);
        }

        public static IServiceProvider BuildServiceProvider(this IDictionary<int, IServiceAccessor> serviceAccessorDictionary)
        {
            InitializeServiceAccessors(serviceAccessorDictionary.Values);
            return new ServiceProvider(serviceAccessorDictionary);
        }

        private static void InitializeServiceAccessors(ICollection<IServiceAccessor> accessors)
        {
            foreach (var serviceAccessor in accessors)
            {
                if (serviceAccessor is IInitializable initializable)
                {
                    initializable.Initialize();
                }
            }
        }
    }
}