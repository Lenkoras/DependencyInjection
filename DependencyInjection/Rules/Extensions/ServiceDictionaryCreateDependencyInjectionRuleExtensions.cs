namespace DependencyInjection.Rules.Extensions
{
    using Accessors;

    public static class ServiceDictionaryCreateDependencyInjectionRuleExtensions
    {
        /// <summary>
        /// Creates a new instance of the <see cref="DependencyInjectionRule"/> from specified <paramref name="serviceAccessorDictionary"/> and <paramref name="maxDependencyInjectionDepth"/>.
        /// </summary>
        /// <param name="serviceAccessorDictionary"></param>
        /// <param name="maxDependencyInjectionDepth"></param>
        /// <returns>A new instance of the <see cref="DependencyInjectionRule"/>.</returns>
        public static DependencyInjectionRule CreateDependencyInjectionRule(this IDictionary<int, IServiceAccessor> serviceAccessorDictionary, int maxDependencyInjectionDepth = DependencyInjectionRule.DefaultMaxDependencyInjectionDepth) =>
            new DependencyInjectionRule(BuildServiceDependencies(serviceAccessorDictionary), maxDependencyInjectionDepth);

        private static IDictionary<int, IEnumerable<int>> BuildServiceDependencies(IDictionary<int, IServiceAccessor> serviceAccessorDictionary)
        {
            ArgumentNullException.ThrowIfNull(serviceAccessorDictionary);

            Dictionary<int, IEnumerable<int>> controllerDictionary = new();

            foreach (var pair in serviceAccessorDictionary)
            {
                IServiceAccessor serviceAccessor = pair.Value;

                if (serviceAccessor is IDependencyInjectionController controller)
                {
                    controllerDictionary.Add(pair.Key, controller.GetDependencies());
                }
            }
            return controllerDictionary;
        }
    }
}
