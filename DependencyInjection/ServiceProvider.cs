namespace DependencyInjection
{
    using Accessors;
    using Rules;
    using Rules.Extensions;

    internal class ServiceProvider : IServiceProvider
    {
        private IDictionary<int, IServiceAccessor> serviceAccessorDictionary { get; }

        public ServiceProvider(IDictionary<int, IServiceAccessor> serviceAccessorDictionary)
        {
            ArgumentNullException.ThrowIfNull(serviceAccessorDictionary);
            serviceAccessorDictionary.CreateDependencyInjectionRule().EnsureCompliance();

            this.serviceAccessorDictionary = serviceAccessorDictionary;
        }

        public ServiceProvider(IDictionary<int, IServiceAccessor> serviceAccessorDictionary, IEnumerable<IRule> ruleCollection)
        {
            ArgumentNullException.ThrowIfNull(serviceAccessorDictionary);
            ArgumentNullException.ThrowIfNull(ruleCollection);

            this.serviceAccessorDictionary = serviceAccessorDictionary;

            if (!ruleCollection.Any(rule => rule is IDependencyInjectionRule))
            {
                List<IRule> ruleList = new List<IRule>() { serviceAccessorDictionary.CreateDependencyInjectionRule() };
                ruleList.AddRange(ruleCollection);
                ruleCollection = ruleList;
            }
            EnsureCompliance(ruleCollection);
        }

        public object? GetService(Type serviceType) =>
            serviceAccessorDictionary.TryGetValue(serviceType.GetHashCode(), out IServiceAccessor? factory) ? factory.GetService(this) : null;

        private static void EnsureCompliance(IEnumerable<IRule> ruleCollection)
        {
            foreach (var rule in ruleCollection)
            {
                rule.EnsureCompliance();
            }
        }
    }
}